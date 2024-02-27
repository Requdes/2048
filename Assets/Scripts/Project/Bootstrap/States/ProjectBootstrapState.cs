public class ProjectBootstrapState : IState {
    private ProjectStateMachine _stateMachine;

    public ProjectBootstrapState (ProjectStateMachine stateMachine) {
        _stateMachine = stateMachine;
    }
    
    public void Enter () {
        _stateMachine.Enter<GameLoadState>();
    }
}
