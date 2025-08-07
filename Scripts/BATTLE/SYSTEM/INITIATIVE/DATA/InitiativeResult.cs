namespace BATTLE.SYSTEM.INITIATIVE.DATA
{
    internal class InitiativeResult
    {
        private ResultType Result;
        public enum ResultType
        {
            Heads,
            Tails
        }
        
        public void SetResult(int resultIndex) => Result = resultIndex == 1 ? ResultType.Heads : ResultType.Tails;
        public ResultType GetResult() => Result;
    }
}