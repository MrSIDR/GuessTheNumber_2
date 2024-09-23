namespace GuessTheNumber.Match
{
    public class PlayerAnswer
    {
        public bool IsCharacter { get; }
        public int Answer { get; }

        public PlayerAnswer(bool isCharacter, int answer)
        {
            IsCharacter = isCharacter;
            Answer = answer;
        }
    }
}