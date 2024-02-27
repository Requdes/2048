using DG.Tweening;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(SkinnedMeshRenderer))]
public class BaseMergingCubeView : MonoBehaviour {
    [SerializeField] private TMP_Text _value;
    [SerializeField] private SkinnedMeshRenderer _mesh;

    private Tweener _curretTransformation;
    private float _currentRoundingWeight = 100f;

    public void SetStyle (CubeStyleData style) {
        _value.text = style.Value;
        _mesh.material = style.Material;
    }

    public void Align () => SetRoundingWeight(0f);
    public void Align (float duration) => DoRoundingWeight(0, duration);

    public void Round () => SetRoundingWeight(100f);
    public void Round (float duration) => DoRoundingWeight(100f, duration);

    private void SetRoundingWeight (float weight) {
        StopTransformation();

        _mesh.SetBlendShapeWeight(0, weight);
        _currentRoundingWeight = weight;
    }

    private void DoRoundingWeight (float value, float duration) {
        StopTransformation();

        var tweener = DOVirtual.Float(_currentRoundingWeight, value, duration, SetWeight).SetLink(gameObject);
        _curretTransformation = tweener;

        void SetWeight (float weight) {
            _mesh.SetBlendShapeWeight(0, weight);
            _currentRoundingWeight = weight;
        }
    }

    private void StopTransformation () {
        if (_curretTransformation == null) return;

        _curretTransformation.Kill();
        _curretTransformation = null;
    }

    public void SetScale (Vector3 scale) {
        transform.localScale = scale;
    }

    public void DoScale (Vector3 scale, float duration) {
        transform.DOScale(scale, duration).SetEase(Ease.InQuad).SetLink(gameObject);
    }

    public void DoChangeColor (Color color, float duration) { 
        _mesh.material.DOColor(color, duration).SetLink(gameObject); 
    }
}
