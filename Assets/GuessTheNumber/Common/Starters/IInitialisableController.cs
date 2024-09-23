namespace GuessTheNumber.Common.Starters
{
    public interface IInitialisableController
    {
        public int InitOrder { get; }
        public void Init();
    }
}