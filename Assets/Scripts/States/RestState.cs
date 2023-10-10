using UnityEngine;

public class RestState : State
{
    public RestState(HunterAgent agent) : base(agent)
    {
    }

    public override void OnEnter()
    {
        _renderer.material.color = Color.green;

        _agent.restTime = 0f;
        _agent.ClearTargets();
    }

    public override void OnExit()
    {
    }

    public override void OnUpdate()
    {
        _agent.restTime += Time.deltaTime;
        if (_agent.restTime >= _agent.energyCooldown)
        {
            _agent.currEnergy = _agent.maxEnergy;
            fsm.ChangeState(FiniteStateMachine.HunterAgentStates.Patrol);
        }
    }
}
