using UnityEngine;

public abstract class State
{
    protected HunterAgent _agent;
    protected Renderer _renderer;

    public State(HunterAgent agent)
    {
        _agent = agent;
        _renderer = _agent.GetComponent<Renderer>();
    }

    public abstract void OnEnter();
    public abstract void OnUpdate();
    public abstract void OnExit();

    public FiniteStateMachine fsm;
}
