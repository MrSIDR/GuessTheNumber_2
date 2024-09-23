using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GuessTheNumber.MatchInput
{
    public class InputNumberButton : MonoBehaviour
    {
        [SerializeField] private Button m_NumberButton;
        [SerializeField] private TMP_Text m_NumberText;
        
        private int m_Number;
        private Action<int> m_OnClickCallback;

        public void Init(Action<int> onClickCallback)
        {
            m_OnClickCallback = onClickCallback;
            
            m_NumberButton.onClick.RemoveListener(ClickButton);
            m_NumberButton.onClick.AddListener(ClickButton);
        }

        public void SetupButton(int number)
        {
            m_Number = number;
            m_NumberText.text = m_Number.ToString();
        }

        private void ClickButton()
        {
            m_OnClickCallback?.Invoke(m_Number);
        }

        private void OnDestroy()
        {
            m_NumberButton.onClick.RemoveListener(ClickButton);
        }
    }
}