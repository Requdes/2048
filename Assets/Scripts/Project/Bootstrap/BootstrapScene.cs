using UnityEngine;
using Zenject;

public class BootstrapScene : MonoBehaviour {
    private ProjectStateMachine _machine;
    private StatesFactory _factory;
    
    [Inject]
    public void Constract (ProjectStateMachine machine, StatesFactory factory) {
        _machine = machine;
        _factory = factory;
    }
    
    private void Start () {
        _machine.RegisterState(_factory.Create<ProjectBootstrapState>());
        _machine.RegisterState(_factory.Create<GameLoadState>());

        _machine.Enter<ProjectBootstrapState>();
    }
}
