using UnityEngine;
using Zenject;

public class ProjectInstaller : MonoInstaller {
    [SerializeField] private StaticDataService _staticData;

    public override void InstallBindings () {
        BindStaticData();

        BindLogger();
        BindCoroutineService();
        
        BindStateMashine();
        BindStateFactory();
    }
    private void BindLogger () {
        Container.Bind<ILogger>().To<UnityDebugLogger>().AsSingle();
    }

    private void BindStaticData () {
        Container.Bind<IStaticDataService>().FromInstance(_staticData).AsSingle();;
    }

    private void BindCoroutineService () {
        Container
            .Bind<ICoroutineRunner>()
            .To<CoroutineRunner>()
            .FromNewComponentOnNewPrefabResource(InfrastructureAssetsPath.CoroutineRunnerPath)
            .AsSingle();
    }

    private void BindStateMashine () {
        Container.Bind<ProjectStateMachine>().To<ProjectStateMachine>().AsSingle();
    }

    private void BindStateFactory () {
        Container.Bind<StatesFactory>().To<StatesFactory>().AsSingle();
    }
}
