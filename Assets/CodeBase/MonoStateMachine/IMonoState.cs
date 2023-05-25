namespace CodeBase.MonoStateMachine
{
    public interface IMonoState
    {
        public void EnterBehavior();
        public void ExitBehavior();
    }
}