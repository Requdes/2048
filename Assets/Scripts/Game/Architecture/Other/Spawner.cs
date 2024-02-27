using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Spawner : MonoBehaviour, ISpawnerService {
    [SerializeField] private List<Transform> _spawnPoints;
    
    private IMergingCubeFactoryService _factory;
    private IStaticDataService _staticData;

    [Inject]
    public void Constract (IMergingCubeFactoryService factory, IStaticDataService staticData) {
        _factory = factory;
        _staticData = staticData;
    }

    public void Spawn () {
        var rangeValues = _staticData.GameSettings.RangeValuesCreating;
        var mergingCube = _factory.Create(Random.Range(rangeValues.x, rangeValues.y));
        var selectedPointIndex = Random.Range(0, _spawnPoints.Count);
    
        mergingCube.transform.position = _spawnPoints[selectedPointIndex].transform.position;
    }
}
