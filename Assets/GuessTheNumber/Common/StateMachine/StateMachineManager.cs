namespace GuessTheNumber.Common.StateMachine
{
    public class StateMachineManager<TState, TTrigger>
    {
        private readonly StateMachine<TState, TTrigger> m_StateMachine;

        public StateMachineManager()
        {
            m_StateMachine = new StateMachine<TState, TTrigger>();
        }
        
        public StateBuilder<TState, TTrigger> Declare(TState state)
        {
            return m_StateMachine.Declare(state);
        }

        public void ChangeState(TTrigger trigger)
        {
            m_StateMachine.ChangeState(trigger);
        }
        
        public void StartMachine()
        {
            m_StateMachine.StartMachine();
        }
    }
}