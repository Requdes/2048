using Zenject;

public class StatesFactory {
    private DiContainer _diContainer;

    public StatesFactory (DiContainer diContainer) {
        _diContainer = diContainer;
    }

    public TState Create<TState> () where TState : class, IState { 
        return _diContainer.Instantiate<TState>();
    }
}
