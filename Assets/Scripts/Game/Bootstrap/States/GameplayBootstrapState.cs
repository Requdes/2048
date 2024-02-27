public class GameplayBootstrapState : IState {
    private GameplayStateMashine _stateMachine;

    public GameplayBootstrapState (GameplayStateMashine stateMachine) {
        _stateMachine = stateMachine;
    }
    
    public void Enter () {
        _stateMachine.Enter<GameplayState>();
    }
}
