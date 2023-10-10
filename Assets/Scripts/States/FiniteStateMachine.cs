using System.Collections.Generic;
using UnityEngine;

public class FiniteStateMachine 
{
    public enum HunterAgentStates
    {
        Rest,
        Patrol,
        Chase
    }

    private State _currentState;
    private Dictionary<HunterAgentStates, State> _allStates = new Dictionary<HunterAgentStates, State>();

    public void Update()
    {
        _currentState?.OnUpdate();
    }

    public void AddState(HunterAgentStates agentState, State state)
    {
        if (!_allStates.ContainsKey(agentState))
        {
            _allStates.Add(agentState, state);
            state.fsm = this;
        }
        else
        {
            _allStates[agentState] = state;
        }
    }

    public void ChangeState(HunterAgentStates agentState)
    {
        _currentState?.OnExit();
        if (_allStates.ContainsKey(agentState)) _currentState = _allStates[agentState];
        _currentState?.OnEnter();
    }
}
