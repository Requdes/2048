using UnityEngine;
using System;

[CreateAssetMenu(fileName = "GameSettings", menuName = "Data/GameSettings")]
public class GameSettings : ScriptableObject {
    
    public Vector2Int RangeValuesCreating => new Vector2Int (0, _rangeValuesCreating);

    [Range(0, 12)]
    [SerializeField] private int _rangeValuesCreating;
}