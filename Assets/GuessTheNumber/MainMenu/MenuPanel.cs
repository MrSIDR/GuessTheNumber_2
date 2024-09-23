using TMPro;
using UnityEngine;

namespace GuessTheNumber.MainMenu
{
    public class MenuPanel : MonoBehaviour
    {
        [SerializeField] private TMP_InputField m_MinValue;
        [SerializeField] private TMP_InputField m_MaxValue;
        [SerializeField] private StartMatchButton m_StartMatchButton;
        
        public StartMatchButton StartMatchButton => m_StartMatchButton;

        public void SetActive(bool isActive)
        {
            gameObject.SetActive(isActive);
        }

        public int GetMinValue()
        {
            return int.Parse(m_MinValue.text);
        }
        
        public int GetMaxValue()
        {
            return int.Parse(m_MaxValue.text);
        }
    }
}