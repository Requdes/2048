using UnityEngine;
using Zenject;

public class GameplayInstaller : MonoInstaller {
    [SerializeField] private Spawner _spawner;

    public override void InstallBindings () {
        BindInput();

        BindSpawner();

        BindMovementDraggedObject();
        BindMerger();
    }

    private void BindInput () {
        Container.BindInterfacesTo<PlayerInputHandler>().AsSingle();
    }

    private void BindSpawner () {
        Container.Bind<ISpawnerService>().FromInstance(_spawner).AsSingle();
    }

    private void BindMovementDraggedObject () {
        Container.BindInterfacesTo<DraggableObjectSelector>().AsSingle();
        Container.BindInterfacesTo<DraggableObjectMover>().AsSingle();
    }

    private void BindMerger () {
        Container.BindInterfacesTo<CollisionDetector<BaseMergingCube, BaseMergingCube>>().AsSingle();
        Container.BindInterfacesTo<Merger>().AsSingle();
    }
}
