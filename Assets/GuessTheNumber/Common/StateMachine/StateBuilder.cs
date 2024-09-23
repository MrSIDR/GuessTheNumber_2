using System;

namespace GuessTheNumber.Common.StateMachine
{
    public class StateBuilder<TState, TTrigger>
    {
        private readonly StateNode<TState, TTrigger> m_Node;
        private readonly StateMachine<TState, TTrigger> m_StateMachine;

        public StateBuilder(StateMachine<TState, TTrigger> stateMachine, TState state)
        {
            m_StateMachine = stateMachine;
            m_Node = new StateNode<TState, TTrigger>(state);
        }

        public StateBuilder<TState, TTrigger> SetInitialisingState()
        {
            m_Node.IsInitialisingState = true;
            return this;
        }
        
        public StateBuilder<TState, TTrigger> AddTransition(TTrigger trigger, TState state)
        {
            m_Node.Transitions.TryAdd(trigger, state);
            return this;
        }
        
        public StateBuilder<TState, TTrigger> AddEntryAction(Action action)
        {
            m_Node.EntryActions.Add(action);
            return this;
        }
        
        public StateBuilder<TState, TTrigger> AddExitAction(Action action)
        {
            m_Node.ExitActions.Add(action);
            return this;
        }

        public void Submit()
        {
            m_StateMachine.SubmitState(m_Node);
        }
    }
}