namespace GuessTheNumber.Match
{
    public class TurnAnswer
    {
        public bool IsWinner { get; }
        public bool IsNumberGreater { get; }

        public static TurnAnswer Winner()
        {
            return new TurnAnswer(true, false);
        }

        public static TurnAnswer NumberGreater()
        {
            return new TurnAnswer(false, true);
        }

        public static TurnAnswer NumberLess()
        {
            return new TurnAnswer(false, false);
        }

        private TurnAnswer(bool isWinner, bool isNumberGreater)
        {
            IsWinner = isWinner;
            IsNumberGreater = isNumberGreater;
        }
    }
}