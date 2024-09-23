using System;
using System.Collections.Generic;

namespace GuessTheNumber.Common.StateMachine
{
    public class StateNode<TState, TTrigger>
    {
        public bool IsInitialisingState;
        
        public readonly Dictionary<TTrigger, TState> Transitions = new();
        public readonly HashSet<Action> EntryActions = new();
        public readonly HashSet<Action> ExitActions = new();
        public TState State { get; }
        
        public StateNode(TState state)
        {
            State = state;
        }
    }
}