public class MergingCubeFactoryService : IMergingCubeFactoryService {
    private IMergingCubeFactory _factory;
    private ICollisingObjectRegistry _collisingRegistry;

    public MergingCubeFactoryService (IMergingCubeFactory factory, ICollisingObjectRegistry collisingRegistry) {
        _factory = factory;
        _collisingRegistry = collisingRegistry;
    }
    
    public BaseMergingCube Create (int index) {
        var newCube = _factory.Create(index);
        
        _collisingRegistry.Add(newCube);

        return newCube;
    }

    public BaseMergingCube CreateImproved (BaseMergingCube cube) {
        return Create(cube.MergingIndex + 1);
    }

    public void Remove (BaseMergingCube cube) {
        _collisingRegistry.Remove(cube);
        _factory.Remove(cube);
    }
}
