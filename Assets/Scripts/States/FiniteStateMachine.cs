using System.Collections.Generic;
using UnityEngine;

public class FiniteStateMachine 
{
    public enum NPCHunterAgentStates
    {
        Rest,
        Patrol,
        Chase
    }

    private State _currentState;
    private Dictionary<NPCHunterAgentStates, State> _allStates = new Dictionary<NPCHunterAgentStates, State>();

    public void Update()
    {
        _currentState?.OnUpdate();
    }

    public void AddState(NPCHunterAgentStates agentState, State state)
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

    public void ChangeState(NPCHunterAgentStates agentState)
    {
        _currentState?.OnExit();
        if (_allStates.ContainsKey(agentState)) _currentState = _allStates[agentState];
        _currentState?.OnEnter();
    }
}
