using UnityEngine;

public class HunterAgent : SteeringAgent
{
    [SerializeField] float _distanceToHunt;

    FiniteStateMachine _fsm;

    private void Start()
    {
        _fsm = new FiniteStateMachine();
        _fsm.AddState(FiniteStateMachine.HunterAgentStates.Rest, new RestState(this));
        _fsm.AddState(FiniteStateMachine.HunterAgentStates.Patrol, new PatrolState(this));
        _fsm.AddState(FiniteStateMachine.HunterAgentStates.Chase, new ChaseState(this));

        _fsm.ChangeState(FiniteStateMachine.HunterAgentStates.Patrol);
    }

    private void Update()
    {
        _fsm.Update();
    }
}
