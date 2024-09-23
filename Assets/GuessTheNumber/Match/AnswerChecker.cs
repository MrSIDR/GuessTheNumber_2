namespace GuessTheNumber.Match
{
    public class AnswerChecker
    {
        private readonly IRandomNumberService m_RandomNumberService;
        
        private int m_Number;

        public AnswerChecker(IRandomNumberService randomNumberService)
        {
            m_RandomNumberService = randomNumberService;
        }

        public void CreateNextNumber()
        {
            m_Number = m_RandomNumberService.GetRandomNumber();
        }
        
        public TurnAnswer CheckNumber(int number)
        {
            if (m_Number == number)
            {
                return TurnAnswer.Winner();
            }

            if (number > m_Number)
            {
                return TurnAnswer.NumberGreater();
            }
            
            return TurnAnswer.NumberLess();
        }
    }
}