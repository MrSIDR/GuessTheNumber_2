using GuessTheNumber.Common.Events;
using GuessTheNumber.Match.Signals;
using GuessTheNumber.MatchInput;

namespace GuessTheNumber.Match.MatchStates
{
    public class YourTurnState : IMatchState
    {
        private readonly IEventManager m_EventManager;
        private readonly MatchInputPanel m_InputPanel;
        
        public YourTurnState(IEventManager eventManager, MatchInputPanel inputPanel)
        {
            m_EventManager = eventManager;
            m_InputPanel = inputPanel;
            m_InputPanel.Init(GetAnswer);
        }
        
        public void StartState()
        {
            m_InputPanel.SetActive(true);
        }

        public void EndState()
        {
            m_InputPanel.SetActive(false);
            m_InputPanel.RefreshNumbers();
        }

        private void GetAnswer(int answer)
        {
            m_EventManager.Trigger<AnswerSignal, PlayerAnswer>(new PlayerAnswer(true, answer));
        }
    }
}