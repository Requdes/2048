using UnityEngine;
using Zenject;

public class GameplaySceneBootstrap : MonoBehaviour {
    private GameplayStateMashine _stateMachine;
    private StatesFactory _statesFactory;

    [Inject]
    public void Constract (GameplayStateMashine stateMachine, StatesFactory statesFactory) {
        _stateMachine = stateMachine;
        _statesFactory = statesFactory;
    }
    
    private void Awake() {
        _stateMachine.RegisterState(_statesFactory.Create<GameplayBootstrapState>());
        _stateMachine.RegisterState(_statesFactory.Create<GameplayState>());

        _stateMachine.Enter<GameplayBootstrapState>();
    }
}
