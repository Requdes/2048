using UnityEngine;
using Zenject;

public class GameplayInfrastructureInstaller : MonoInstaller {
    [SerializeField] private MergingCubeFactory _mergingCubeFactory;
    [SerializeField] private MergenCubeStylesData _mergenCubeGradeData;

    private CubeStylistService _cubeStylistService;

    public override void InstallBindings() {
        BindStyleService();
        BindStorageService();
        BindFactoryService();
        BindRaycastService();
        
        BindStateMashine();
        BindStateFactory();
    }

    private void BindStyleService () {
        _cubeStylistService = new CubeStylistService (_mergenCubeGradeData);
        Container.Bind<ICubeStylistService>().FromInstance(_cubeStylistService).AsSingle();
    }

    private void BindStorageService () {
        Container.Bind<ICollisingObjectRegistry>().To<CollisingObjectRegistry>().AsSingle();
    }

    private void BindFactoryService () {
        _mergingCubeFactory.Init(_cubeStylistService);
        Container.Bind<IMergingCubeFactory>().FromInstance(_mergingCubeFactory).AsSingle();
        Container.Bind<IMergingCubeFactoryService>().To<MergingCubeFactoryService>().AsSingle();
    }

    private void BindRaycastService () {
        Container.Bind<IRaycastService>().To<RaycastService>().AsSingle();
    }

    private void BindStateMashine () {
        Container.Bind<GameplayStateMashine>().AsSingle();
    }

    private void BindStateFactory () {
        Container.Bind<StatesFactory>().AsSingle();
    }
}
