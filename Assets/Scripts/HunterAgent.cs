using UnityEngine;

public class HunterAgent : SteeringAgent
{
    public float huntDistance;
    public float arriveDistance;

    private Transform _arriveTarget = null;
    private SteeringAgent _pursuitTarget = null;

    FiniteStateMachine _fsm;

    private void Start()
    {
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
        _fsm.Update();

        if (_arriveTarget)
        {
            if (!UseAvoidance()) AddForce(Arrive(_arriveTarget.position));
        }
        else if (_pursuitTarget)
        {
            if (!UseAvoidance()) AddForce(Pursuit(_pursuitTarget));
        }

        Move();
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
