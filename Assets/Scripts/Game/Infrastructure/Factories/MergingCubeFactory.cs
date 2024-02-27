using UnityEngine;

[CreateAssetMenu(fileName = "MergingCubeFactory", menuName = "Data/Factory/MergingCubeFactory")]
public class MergingCubeFactory : ScriptableObject, IMergingCubeFactory {
    [SerializeField] private BaseMergingCube _prefab;

    private ICubeStylistService _stylistService;

    public void Init (ICubeStylistService stylistService) {
        _stylistService = stylistService;
    }

    public BaseMergingCube Create (int index) {
        var merginData = new MerginData();
        var mergenCube = Instantiate(_prefab, Vector3.zero, Quaternion.identity);
        var style = _stylistService.GetStyle(index);

        merginData.MergingIndex = index;

        mergenCube.Init(merginData);
        mergenCube.SetStyle(style);

        return mergenCube;
    }

    public void Remove (BaseMergingCube cube) {
        Destroy(cube.gameObject);
    }
}
