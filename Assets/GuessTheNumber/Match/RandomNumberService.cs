using UnityEngine;

namespace GuessTheNumber.Match
{
    public class RandomNumberService : IRandomNumberService
    {
        private int m_MinNumber;
        private int m_MaxNumber;

        public RandomNumberService(int min, int max)
        {
            if (min > max)
            {
                (max, min) = (min, max);
            }

            m_MinNumber = min;
            m_MaxNumber = max;
        }

        public int GetRandomNumber()
        {
            return Random.Range(m_MinNumber, m_MaxNumber);
        }
    }
}