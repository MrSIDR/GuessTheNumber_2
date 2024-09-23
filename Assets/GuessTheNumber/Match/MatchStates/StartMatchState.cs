using System;
using GuessTheNumber.Common.Events;
using GuessTheNumber.Match.Signals;
using GuessTheNumber.MatchAnswer;
using Random = UnityEngine.Random;

namespace GuessTheNumber.Match.MatchStates
{
    public class StartMatchState : IMatchState
    {
        private readonly MatchAnswerPanel m_MatchAnswerPanel;
        private readonly AnswerChecker m_AnswerChecker;
        private readonly IEventManager m_EventManager;

        public StartMatchState(
            MatchAnswerPanel matchAnswerPanel,
            AnswerChecker answerChecker,
            IEventManager eventManager)
        {
            m_MatchAnswerPanel = matchAnswerPanel;
            m_AnswerChecker = answerChecker;
            m_EventManager = eventManager;
        }

        public void StartState()
        {
            m_MatchAnswerPanel.Init();
            m_MatchAnswerPanel.SetActive(true);
            m_AnswerChecker.CreateNextNumber();

            var firstTurn = Random.Range(0, 1);
            var trigger = firstTurn == 0 ? nameof(YourTurnTrigger) : nameof(OpponentTurnTrigger);
            m_EventManager.Trigger<ChangeStateSignal, string>(trigger);
        }

        public void EndState()
        {
            
        }
    }
}