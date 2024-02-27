using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MergenCubeStylesData", menuName = "Data/MergenCube/stylesData")]
public class MergenCubeStylesData : ScriptableObject {
    [SerializeField] private List<CubeStyleData> _stylesData;

    public CubeStyleData GetStyle (int index) => _stylesData[index];
} 