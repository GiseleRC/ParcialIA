using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : State
{
    private NPCHunterAgent _agent;

    public ChaseState(NPCHunterAgent agent)
    {
        _agent = agent;
    }

    public override void OnEnter()
    {
    }

    public override void OnExit()
    {
    }

    public override void OnUpdate()
    {
    }
}
