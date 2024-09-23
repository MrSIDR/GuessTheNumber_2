using GuessTheNumber.Common.Events;
using GuessTheNumber.Common.Starters;
using GuessTheNumber.Match;
using GuessTheNumber.Match.Signals;
using GuessTheNumber.MatchAnswer;
using GuessTheNumber.MatchInput;
using UnityEngine;
using Zenject;

namespace GuessTheNumber.MainMenu
{
    public class CoreUIController : MonoBehaviour, IInitialisableController
    {
        [SerializeField] private MenuPanel m_MenuPanel;
        [SerializeField] private MatchInputPanel m_MatchInputPanel;
        [SerializeField] private MatchAnswerPanel m_MatchAnswerPanel;

        [Inject] private IEventManager m_EventManager;
        
        public MenuPanel MenuPanel => m_MenuPanel;
        public MatchInputPanel MatchInputPanel => m_MatchInputPanel;
        public MatchAnswerPanel MatchAnswerPanel => m_MatchAnswerPanel;
        
        public int InitOrder => 0;
        
        public void Init()
        {
            Debug.Log($"Init {nameof(CoreUIController)}");
            m_EventManager.Subscribe<StartMatchSignal>(this, MatchStart);
            m_EventManager.Subscribe<FinishMatchSignal>(this, MatchFinish);
        }

        private void MatchStart()
        {
            m_MenuPanel.SetActive(false);
        }
        
        private void MatchFinish()
        {
            m_MenuPanel.SetActive(true);
        }
    }
}
