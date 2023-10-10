using UnityEngine;

public class ChaseState : State
{
    public ChaseState(HunterAgent agent) : base(agent)
    {
    }

    public override void OnEnter()
    {
        _renderer.material.color = Color.red;
    }

    public override void OnExit()
    {
    }

    public override void OnUpdate()
    {
        //energyLeft -= energyDrain * Time.deltaTime;
        //if (energyLeft <= 0f)
        //{
        //    fsm.ChangeState(FiniteStateMachine.HunterAgentStates.Rest);
        //    return;
        //}

        BoidAgent nearestBoidAgent = null;

        foreach (BoidAgent boidAgent in GameManager.instance.allBoidAgents)
        {
            if (!nearestBoidAgent || (boidAgent.transform.position - _agent.transform.position).sqrMagnitude < (nearestBoidAgent.transform.position - _agent.transform.position).sqrMagnitude)
                nearestBoidAgent = boidAgent;
        }

        if (nearestBoidAgent && (nearestBoidAgent.transform.position - _agent.transform.position).magnitude < _agent.huntDistance)
            _agent.SetPursuitTarget(nearestBoidAgent);
        else
            fsm.ChangeState(FiniteStateMachine.HunterAgentStates.Patrol);
    }
}
