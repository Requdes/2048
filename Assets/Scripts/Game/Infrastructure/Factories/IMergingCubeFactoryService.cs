public interface IMergingCubeFactoryService {
    BaseMergingCube Create (int index);
    BaseMergingCube CreateImproved (BaseMergingCube cube);
    void Remove (BaseMergingCube cube);
}
