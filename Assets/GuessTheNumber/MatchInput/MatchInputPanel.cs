using System;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace GuessTheNumber.MatchInput
{
    public class MatchInputPanel : MonoBehaviour
    {
        [SerializeField] private InputNumberButton[] m_NumberButtons;
        [SerializeField] private Button m_AnswerButton;
        [SerializeField] private Button m_ResetButton;
        [SerializeField] private TMP_Text m_AnswerText;

        private int m_Number;
        private int[] m_Numbers;
        private StringBuilder m_StringBuilder = new();
        private Action<int> m_AnswerCallback;

        public void Init(Action<int> answerCallback)
        {
            m_AnswerCallback = answerCallback;

            m_Numbers = new int[m_NumberButtons.Length];
            for (int i = 0; i < m_NumberButtons.Length; i++)
            {
                m_NumberButtons[i].Init(AddNumber);
                m_NumberButtons[i].SetupButton(i);
                m_Numbers[i] = i;
            }
            
            m_ResetButton.onClick.RemoveListener(ResetNumber);
            m_ResetButton.onClick.AddListener(ResetNumber);
            
            m_AnswerButton.onClick.RemoveListener(AnswerClick);
            m_AnswerButton.onClick.AddListener(AnswerClick);
        }

        public void SetActive(bool isActive)
        {
            gameObject.SetActive(isActive);
        }

        public void RefreshNumbers()
        {
            for (var i = 0; i < m_Numbers.Length; i++)
            {
                var randomIndex = Random.Range(0, m_Numbers.Length);  
                (m_Numbers[randomIndex], m_Numbers[i]) = (m_Numbers[i], m_Numbers[randomIndex]);
            }

            for (int i = 0; i < m_Numbers.Length; i++)
            {
                m_NumberButtons[i].SetupButton(m_Numbers[i]);
            }
        }
        
        private void ResetNumber()
        {
            if (m_StringBuilder.Length == 0)
            {
                return;
            }
            
            m_StringBuilder.Length--;
            m_AnswerText.text = m_StringBuilder.ToString();
        }
        
        private void AddNumber(int number)
        {
            if (m_StringBuilder.Length >= 10)
            {
                return;
            }

            m_StringBuilder.Append(number);
            m_AnswerText.text = m_StringBuilder.ToString();
        }

        private void AnswerClick()
        {
            if (int.TryParse(m_StringBuilder.ToString(), out var result))
            {
                m_AnswerCallback?.Invoke(result);
                m_StringBuilder.Clear();
                m_AnswerText.text = string.Empty;
            }
        }

        private void OnDestroy()
        {
            m_ResetButton.onClick.RemoveListener(AnswerClick);
            m_AnswerButton.onClick.RemoveListener(AnswerClick);
        }
    }
}