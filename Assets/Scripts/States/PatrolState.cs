using UnityEngine;

public class PatrolState : State
{
    private int _wayPointIdx = 0;

    public PatrolState(HunterAgent agent) : base(agent)
    {
    }

    public override void OnEnter()
    {
        _renderer.material.color = Color.yellow;
    }

    public override void OnExit()
    {
    }

    public override void OnUpdate()
    {
        _agent.currEnergy -= _agent.energyDrain * Time.deltaTime;
        if (_agent.currEnergy <= 0f)
        {
            fsm.ChangeState(FiniteStateMachine.HunterAgentStates.Rest);
            return;
        }

        BoidAgent nearestBoidAgent = null;

        foreach (BoidAgent boidAgent in GameManager.instance.allBoidAgents)
        {
            if (!nearestBoidAgent || (boidAgent.transform.position - _agent.transform.position).sqrMagnitude < (nearestBoidAgent.transform.position - _agent.transform.position).sqrMagnitude)
                nearestBoidAgent = boidAgent;
        }

        if (nearestBoidAgent && (nearestBoidAgent.transform.position - _agent.transform.position).magnitude < _agent.huntDistance)
        {
            fsm.ChangeState(FiniteStateMachine.HunterAgentStates.Chase);
            return;
        }

        HunterAgentWayPoint wayPoint = GameManager.instance.allHunterAgentWayPoints[_wayPointIdx];
        if ((wayPoint.transform.position - _agent.transform.position).magnitude < _agent.arriveDistance)
        {
            if (++_wayPointIdx >= GameManager.instance.allHunterAgentWayPoints.Count) _wayPointIdx = 0;
            wayPoint = GameManager.instance.allHunterAgentWayPoints[_wayPointIdx];
        }
        _agent.SetArriveTarget(wayPoint.transform);
    }
}
