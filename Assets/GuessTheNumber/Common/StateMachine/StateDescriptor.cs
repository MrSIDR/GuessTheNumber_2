using System;
using System.Collections.Generic;

namespace GuessTheNumber.Common.StateMachine
{
    public class StateDescriptor<TState, TTrigger>
    {
        private readonly Dictionary<TTrigger, TState> m_Transitions;
        private readonly HashSet<Action> m_EntryActions;
        private readonly HashSet<Action> m_ExitActions;

        private TState m_State;
        private bool m_IsActive;

        public TState State => m_State;
        
        
        public StateDescriptor(
            TState state,
            Dictionary<TTrigger, TState> transitions,
            HashSet<Action> entryActions,
            HashSet<Action> exitActions)
        {
            m_State = state;
            m_ExitActions = new HashSet<Action>();
            m_Transitions = transitions;
            m_EntryActions = entryActions;
            m_ExitActions = exitActions;
        }

        public bool CanHandle(TTrigger trigger)
        {
            var result = m_Transitions.ContainsKey(trigger);
            return result;
        }

        public TState GetTransitionState(TTrigger trigger)
        {
            if (!m_Transitions.ContainsKey(trigger))
            {
                return default;
            }

            return m_Transitions[trigger];
        }

        public void OnEnter()
        {
            if (m_IsActive)
            {
                return;
            }

            m_IsActive = true;
            foreach (var action in m_EntryActions)
            {
                action?.Invoke();
            }
        }

        public void OnExit()
        {
            if (!m_IsActive)
            {
                return;
            }

            m_IsActive = false;
            foreach (var action in m_ExitActions)
            {
                action?.Invoke();
            }
        }
    }
}