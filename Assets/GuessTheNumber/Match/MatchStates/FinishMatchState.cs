using Cysharp.Threading.Tasks;
using GuessTheNumber.Common.Events;
using GuessTheNumber.MainMenu;
using GuessTheNumber.Match.Signals;
using GuessTheNumber.MatchAnswer;
using GuessTheNumber.MatchInput;

namespace GuessTheNumber.Match.MatchStates
{
    public class FinishMatchState : IMatchState
    {
        private const int m_WaitSecond = 5 * 1000;
        
        private readonly MatchAnswerPanel m_MatchAnswerPanel;
        private readonly IEventManager m_EventManager;

        public FinishMatchState(MatchAnswerPanel matchAnswerPanel, IEventManager eventManager)
        {
            m_MatchAnswerPanel = matchAnswerPanel;
            m_EventManager = eventManager;
        }

        public void StartState()
        {
            FinishMatch();
        }

        public void EndState()
        {
            
        }

        private async void FinishMatch()
        {
            await UniTask.Delay(m_WaitSecond);
            
            m_MatchAnswerPanel.SetActive(false);
            
            m_EventManager.Trigger<FinishMatchSignal>();
        }
    }
}