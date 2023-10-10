using UnityEngine;

public class RestState : State
{
    public RestState(HunterAgent agent) : base(agent)
    {
    }

    public override void OnEnter()
    {
        _renderer.material.color = Color.green;
    }

    public override void OnExit()
    {
    }

    public override void OnUpdate()
    {
    }
}
