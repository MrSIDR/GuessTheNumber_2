namespace GuessTheNumber.MatchAnswer
{
    public class MatchAnswerPanelConfig
    {
        public bool IsCharacter { get; }
        public bool IsWinner { get; }
        public bool IsNumberGreater { get; }

        public MatchAnswerPanelConfig(bool isCharacter, bool isWinner, bool isNumberGreater)
        {
            IsCharacter = isCharacter;
            IsWinner = isWinner;
            IsNumberGreater = isNumberGreater;
        }
    }
}