public interface IStateMashine {
    public void RegisterState<TState>(TState state) where TState : class, IState;
    public void Enter<TState> () where TState : class, IState;
}
