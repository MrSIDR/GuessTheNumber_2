using System.Collections.Generic;
using UnityEngine;

namespace GuessTheNumber.Common.StateMachine
{
    public class StateMachine<TState, TTrigger>
    {
        private readonly Dictionary<TState, StateDescriptor<TState, TTrigger>> m_StateDescriptions = new();
        
        private StateDescriptor<TState, TTrigger> m_CurrentDescription;

        public StateBuilder<TState, TTrigger> Declare(TState state)
        {
            var builder = new StateBuilder<TState, TTrigger>(this, state);
            return builder;
        }

        public bool HasState(TState state)
        {
            return m_StateDescriptions.ContainsKey(state);
        }

        public void StartMachine()
        {
            m_CurrentDescription.OnEnter();
        }
        
        public void ChangeState(TTrigger trigger)
        {
            if (m_CurrentDescription == null)
            {
                Debug.LogError($"{nameof(m_CurrentDescription)} is null!");
                return;    
            }
            
            if (!m_CurrentDescription.CanHandle(trigger))
            {
                return;
            }
            
            m_CurrentDescription.OnExit();
            
            var newState = m_CurrentDescription.GetTransitionState(trigger);
            var newStateDescription = GetStateDescription(newState);

            if (newStateDescription == null)
            {
                return;
            }
            
            newStateDescription.OnEnter();
            m_CurrentDescription = newStateDescription;
        }
        
        internal void SubmitState(StateNode<TState, TTrigger> node)
        {
            if (m_StateDescriptions.ContainsKey(node.State))
            {
                Debug.LogError($"State {node.State} already submitted!");
                return;
            }

            var descriptor = new StateDescriptor<TState, TTrigger>(
                node.State,
                node.Transitions,
                node.EntryActions,
                node.ExitActions);
            
            m_StateDescriptions.Add(node.State, descriptor);

            if (node.IsInitialisingState)
            {
                m_CurrentDescription = descriptor;
            }
        }

        private StateDescriptor<TState, TTrigger> GetStateDescription(TState state)
        {
            if (!m_StateDescriptions.ContainsKey(state))
            {
                Debug.LogError($"Description for {state} doesn't exist!");
                return null;
            }

            return m_StateDescriptions[state];
        }
    }
}