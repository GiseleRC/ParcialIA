using UnityEngine;
using UnityEngine.UI;

public class HunterAgent : SteeringAgent
{
    [SerializeField] protected LayerMask _boidAgentsMask;

    public float huntDistance;
    public float arriveDistance;
    public float maxEnergy;
    public float energyDrain;
    public float energyCooldown;

    public float currEnergy;
    public float restTime;

    private Transform _arriveTarget = null;
    private SteeringAgent _pursuitTarget = null;

    private FiniteStateMachine _fsm;

    protected Slider _energySlider;
    
    private void Start()
    {
        _energySlider = FindObjectOfType<Slider>();

        currEnergy = maxEnergy;

        GameManager.instance.allHunterAgents.Add(this);

        _fsm = new FiniteStateMachine();
        _fsm.AddState(FiniteStateMachine.HunterAgentStates.Rest, new RestState(this));
        _fsm.AddState(FiniteStateMachine.HunterAgentStates.Patrol, new PatrolState(this));
        _fsm.AddState(FiniteStateMachine.HunterAgentStates.Chase, new ChaseState(this));

        _fsm.ChangeState(FiniteStateMachine.HunterAgentStates.Patrol);
    }

    private void OnDestroy()
    {
        GameManager.instance.allHunterAgents.Remove(this);
    }

    private void Update()
    {
        _energySlider.value = currEnergy / maxEnergy;

        _fsm.Update();

        if (_arriveTarget)
        {
            if (!UseAvoidance()) AddForce(Arrive(_arriveTarget.position));
        }
        else if (_pursuitTarget)
        {
            if (!UseAvoidance()) AddForce(Pursuit(_pursuitTarget));
        }
        else
        {
            if (!UseAvoidance()) AddForce(Seek(transform.position));
        }

        Move();
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((_boidAgentsMask & 1 << other.gameObject.layer) == 0) return;

        other.gameObject.transform.position = GameManager.instance.GetObstacleFreePosition();
    }

    public void SetArriveTarget(Transform arriveTarget)
    {
        _pursuitTarget = null;
        _arriveTarget = arriveTarget;
    }

    public void SetPursuitTarget(SteeringAgent pursuitTarget)
    {
        _arriveTarget = null;
        _pursuitTarget = pursuitTarget;
    }

    public void ClearTargets()
    {
        _arriveTarget = null;
        _pursuitTarget = null;
    }
}
