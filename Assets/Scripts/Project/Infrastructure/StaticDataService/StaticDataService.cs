using UnityEngine;

[CreateAssetMenu(fileName = "StaticData", menuName = "Data/StaticData")]
public class StaticDataService : ScriptableObject, IStaticDataService {
    public GameSettings GameSettings => _gameSettings;

    [SerializeField] private GameSettings _gameSettings;
}
