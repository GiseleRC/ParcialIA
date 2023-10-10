using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : State
{
    private NPCHunterAgent _agent;

    public PatrolState(NPCHunterAgent agent)
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
