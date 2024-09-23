using GuessTheNumber.Match;

namespace GuessTheNumber.Players.Opponent
{
    public class IaOpponent : IOpponent
    {
        private int m_MinNumber;
        private int m_MaxNumber;
        private int m_LastNumber;
        
        public IaOpponent(int minNumber, int maxNumber)
        {
            m_MinNumber = minNumber;
            m_MaxNumber = maxNumber;
        }
        
        public int GetAnswer()
        {
            m_LastNumber = m_MinNumber + (m_MaxNumber - m_MinNumber) / 2;
            return m_LastNumber;
        }

        public void SetResult(TurnAnswer turnAnswer)
        {
            if (turnAnswer.IsWinner)
            {
                return;
            }

            if (turnAnswer.IsNumberGreater)
            {
                m_MaxNumber = m_LastNumber;
            }
            else
            {
                m_MinNumber = m_LastNumber;
            }
        }
    }
}