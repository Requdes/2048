using UnityEngine;

public class BaseMergingCube : AbstractDraggableObject {
    public int MergingIndex => _merginData.MergingIndex;

    [SerializeField] private BaseMergingCubeView _view;
    [SerializeField] private Rigidbody2D _rigidbody;

    private MerginData _merginData;
    private TargetJoint2D _joint;

    public void Init (MerginData merginData) {
        _merginData = merginData;
    }

    protected override void Awake () {
        base.Awake();

        _joint = gameObject.AddComponent<TargetJoint2D>();
        _joint.autoConfigureTarget = false;
        _joint.enabled = false;
    }

    public override void StartDrag (Vector2 startPoint) {
        _joint.enabled = true;
        _joint.anchor = transform.InverseTransformPoint(startPoint);
        _joint.target = startPoint;
    }

    public override void Drag (Vector2 dragPoint) {
        _joint.target = dragPoint;
    }

    public override void EndDrag (Vector2 dropPoint) {
        _joint.enabled = false;
    }

    public void ActivatePhysics () {
        _rigidbody.isKinematic = false;
    }

    public void DeactivatePhysics () {
        _rigidbody.isKinematic = true;
    }

    public void SetStyle (CubeStyleData styleData) => _view.SetStyle(styleData);

    public void Align () => _view.Align();
    public void DoAlign (float duration) => _view.Align(duration);

    public void Round () => _view.Round();
    public void DoRound (float duration) => _view.Round(duration);

    public void SetScale (Vector3 scale) => _view.SetScale(scale);
    public void DoScale (Vector3 scale, float duration) => _view.DoScale(scale, duration);

    public void DoChangeColor (Color color, float duration) => _view.DoChangeColor(color, duration); 
}

public class MerginData {
    public int MergingIndex;
}