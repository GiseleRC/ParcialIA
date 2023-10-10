using UnityEngine;

public class NPCHunterAgent : SteeringAgent
{
    [SerializeField] float _distanceToHunt;

    FiniteStateMachine _fsm;

    private void Start()
    {
        _fsm = new FiniteStateMachine(); //Esto esta creando una nueva FSM;
        _fsm.AddState(FiniteStateMachine.NPCHunterAgentStates.Rest, new RestState(this));
        _fsm.AddState(FiniteStateMachine.NPCHunterAgentStates.Patrol, new PatrolState(this));
        _fsm.AddState(FiniteStateMachine.NPCHunterAgentStates.Chase, new ChaseState(this));

        _fsm.ChangeState(FiniteStateMachine.NPCHunterAgentStates.Patrol);
    }

    //El hunter utiliza UseAvoidanse, addforce, pursuit, movement, hunt
    private void Update()
    {
        //if (!UseAvoidance()) AddForce(Pursuit(_targetBoidAgent));
        Move();
    }
}
