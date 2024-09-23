using System;
using UnityEngine;
using UnityEngine.UI;

namespace GuessTheNumber.MainMenu
{
    public class StartMatchButton : MonoBehaviour
    {
        [SerializeField] private Button m_StartMatchButton;
        
        private Action m_OnClickCallback;

        public void Init(Action onClickCallback)
        {
            m_OnClickCallback = onClickCallback;
            m_StartMatchButton.onClick.RemoveListener(StartGameClick);
            m_StartMatchButton.onClick.AddListener(StartGameClick);
        }
        
        private void StartGameClick()
        {
            m_OnClickCallback?.Invoke();
        }

        private void OnDestroy()
        {
            m_StartMatchButton.onClick.RemoveListener(StartGameClick);
        }
    }
}