using UnityEngine.SceneManagement;

public class GameLoadState : IState {
    private ProjectStateMachine _stateMachine;

    public GameLoadState (ProjectStateMachine stateMachine) {
        _stateMachine = stateMachine;
    }
    
    public void Enter () {
        SceneManager.LoadScene(InfrastructureAssetsPath.GameplayScene);
    }
}
