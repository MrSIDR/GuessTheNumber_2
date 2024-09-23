using GuessTheNumber.Match;

namespace GuessTheNumber.Players.Opponent
{
    public interface IOpponent
    {
        public int GetAnswer();
        void SetResult(TurnAnswer turnAnswer);
    }
}