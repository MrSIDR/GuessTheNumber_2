using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GuessTheNumber.MatchAnswer
{
    public class MatchAnswerPanel : MonoBehaviour
    {
        [SerializeField] private Color m_WinColor;
        [SerializeField] private Color m_WrongColor;
        [SerializeField] private Color m_DefaultColor;
        
        [SerializeField] private Image m_ColorImage;
        [SerializeField] private TMP_Text m_PlayerText;
        [SerializeField] private TMP_Text m_ResultText;

        public void Init()
        {
            m_ColorImage.color = m_DefaultColor;
            m_PlayerText.text = string.Empty;
            m_ResultText.text = string.Empty;
        }

        public void SetActive(bool isActive)
        {
            gameObject.SetActive(isActive);
        }

        public void SetupView(MatchAnswerPanelConfig config)
        {
            m_ColorImage.color = config.IsWinner 
                ? m_WinColor 
                : m_WrongColor;

            m_PlayerText.text = config.IsCharacter ? "Player" : "Opponent";
            
            m_ResultText.text = config.IsWinner 
                ? "=" 
                : config.IsNumberGreater 
                    ? ">" 
                    : "<";
        }
    }
}