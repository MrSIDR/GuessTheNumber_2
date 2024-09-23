using GuessTheNumber.Common.Events;
using GuessTheNumber.Common.Starters;
using GuessTheNumber.Common.StateMachine;
using GuessTheNumber.MainMenu;
using GuessTheNumber.Match.MatchStates;
using GuessTheNumber.Match.Signals;
using GuessTheNumber.MatchAnswer;
using GuessTheNumber.Players.Opponent;
using UnityEngine;
using Zenject;

namespace GuessTheNumber.Match
{
    public class AiMatchController : MonoBehaviour, IInitialisableController
    {
        [Inject] private IEventManager m_EventManager;
        [Inject] private CoreUIController m_CoreUIController;

        private StateMachineManager<IMatchState, string> m_MachineManager;
        private StartMatchButton m_StartGameButton;
        private StartMatchState m_StartMatchState;
        private YourTurnState m_YourTurnState;
        private OpponentTurnState m_OpponentTurnState;
        private FinishMatchState m_FinishMatchState;
        private AnswerChecker m_AnswerChecker;

        private int m_MaxNumber;
        private int m_MinNumber;
        
        public int InitOrder => 0;

        public void Init()
        {
            m_StartGameButton = m_CoreUIController.MenuPanel.StartMatchButton;
            m_StartGameButton.Init(StartMatch);
        }

        private void StartMatch()
        {
            if (!ValidateNumbers())
            {
                return;
            }
            
            SubscribeMatchEvent();
            
            var randomNumberService = new RandomNumberService(m_MinNumber, m_MaxNumber);
            m_AnswerChecker = new AnswerChecker(randomNumberService);

            m_StartMatchState = new StartMatchState(
                m_CoreUIController.MatchAnswerPanel,
                m_AnswerChecker,
                m_EventManager);
            
            m_YourTurnState = new YourTurnState(
                m_EventManager,
                m_CoreUIController.MatchInputPanel);

            var opponent = new IaOpponent(m_MinNumber, m_MaxNumber);
            m_OpponentTurnState = new OpponentTurnState(opponent, m_EventManager);
            
            m_FinishMatchState = new FinishMatchState(m_CoreUIController.MatchAnswerPanel, m_EventManager);

            CreateMatchStateMachine();
            
            m_EventManager.Trigger<StartMatchSignal>();
            m_MachineManager.StartMachine();
            Debug.Log($"Start match");
        }

        private bool ValidateNumbers()
        {
            var minValue = m_CoreUIController.MenuPanel.GetMinValue();
            var maxValue = m_CoreUIController.MenuPanel.GetMaxValue();

            if (minValue == maxValue)
            {
                return false;
            }
            
            if (minValue > maxValue)
            {
                (minValue, maxValue) = (maxValue, minValue);
            }

            m_MaxNumber = maxValue + 1;
            m_MinNumber = minValue;
            return true;
        }

        private void SubscribeMatchEvent()
        {
            m_EventManager.Unsubscribe<AnswerSignal>(this);                         
            m_EventManager.Subscribe<AnswerSignal, PlayerAnswer>(this, CheckAnswer);
                                                                        
            m_EventManager.Unsubscribe<ChangeStateSignal>(this);                    
            m_EventManager.Subscribe<ChangeStateSignal, string>(this, ChangeState); 
        }
        
        private void CheckAnswer(PlayerAnswer playerAnswer)
        {
            var answer = m_AnswerChecker.CheckNumber(playerAnswer.Answer);
            
            var config = new MatchAnswerPanelConfig(
                playerAnswer.IsCharacter, 
                answer.IsWinner, 
                answer.IsNumberGreater);
            
            m_CoreUIController.MatchAnswerPanel.SetupView(config);
            m_EventManager?.Trigger<AnswerResultSignal, TurnAnswer>(answer);

            if (answer.IsWinner)
            {
                m_MachineManager.ChangeState(nameof(FinishMatchTrigger));
                return;
            }

            var trigger = playerAnswer.IsCharacter 
                ? nameof(OpponentTurnTrigger) 
                : nameof(YourTurnTrigger);
            
            m_MachineManager.ChangeState(trigger);
        }

        private void ChangeState(string trigger)
        {
            m_MachineManager.ChangeState(trigger);
        }
        
        private void CreateMatchStateMachine()
        {
            m_MachineManager = new StateMachineManager<IMatchState, string>();
            m_MachineManager
                .Declare(m_StartMatchState)
                .AddTransition(nameof(YourTurnTrigger), m_YourTurnState)
                .AddTransition(nameof(OpponentTurnTrigger), m_OpponentTurnState)
                .AddEntryAction(m_StartMatchState.StartState)
                .AddExitAction(m_StartMatchState.EndState)
                .SetInitialisingState()
                .Submit();
            
            m_MachineManager
                .Declare(m_YourTurnState)
                .AddTransition(nameof(OpponentTurnTrigger), m_OpponentTurnState)
                .AddTransition(nameof(FinishMatchTrigger), m_FinishMatchState)
                .AddEntryAction(m_YourTurnState.StartState)
                .AddExitAction(m_YourTurnState.EndState)
                .Submit();
            
            m_MachineManager
                .Declare(m_OpponentTurnState)
                .AddTransition(nameof(YourTurnTrigger), m_YourTurnState)
                .AddTransition(nameof(FinishMatchTrigger), m_FinishMatchState)
                .AddEntryAction(m_OpponentTurnState.StartState)
                .AddExitAction(m_OpponentTurnState.EndState)
                .Submit();
            
            m_MachineManager
                .Declare(m_FinishMatchState)
                .AddEntryAction(m_FinishMatchState.StartState)
                .AddExitAction(m_FinishMatchState.EndState)
                .Submit();
        }

        private void OnDestroy()
        {
            m_EventManager?.Unsubscribe<FinishMatchSignal>(this);
            m_EventManager?.Unsubscribe<AnswerSignal>(this);
            m_EventManager?.Unsubscribe<ChangeStateSignal>(this);
        }
    }
}