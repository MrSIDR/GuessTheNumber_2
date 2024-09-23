using Cysharp.Threading.Tasks;
using GuessTheNumber.Common.Events;
using GuessTheNumber.Match.Signals;
using GuessTheNumber.Players.Opponent;

namespace GuessTheNumber.Match.MatchStates
{
    public class OpponentTurnState : IMatchState
    {
        private const int m_WaitSecond = 4 * 1000;
        
        private readonly IEventManager m_EventManager;
        private readonly IOpponent m_Opponent;

        public OpponentTurnState(IOpponent opponent, IEventManager eventManager)
        {
            m_Opponent = opponent;
            m_EventManager = eventManager;
        }

        public void StartState()
        {
            m_EventManager?.Subscribe<AnswerResultSignal, TurnAnswer>(this, SetResult);
            GetAnswer();
        }

        public void EndState()
        {
            m_EventManager?.Unsubscribe<AnswerResultSignal>(this);
        }

        private void SetResult(TurnAnswer answer)
        {
            m_Opponent.SetResult(answer);
        }
        
        private async void GetAnswer()
        {
            await UniTask.Delay(m_WaitSecond);
            
            var answer = m_Opponent.GetAnswer();

            m_EventManager.Trigger<AnswerSignal, PlayerAnswer>(new PlayerAnswer(false, answer));
        } 
    }
}