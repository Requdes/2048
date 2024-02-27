using UnityEngine;

public class Stabilizer : MonoBehaviour {
    [SerializeField] private Transform _pointStabilization;

    private void Update() {
        if (_pointStabilization == null) return;

        transform.localRotation = Quaternion.Inverse(_pointStabilization.localRotation);
    }
}
