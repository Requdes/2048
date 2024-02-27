using UnityEngine;

public abstract class AbstractDraggableObject : AbstractCollisingObject, IDraggable {
    public bool IsDraggeble => _isDraggeble;
    protected bool _isDraggeble = true;

    public virtual void ActivateDraggable () => _isDraggeble = true;
    public virtual void DeactivateDraggable () => _isDraggeble = false;

    public abstract void StartDrag (Vector2 startPoint);
    public abstract void Drag (Vector2 dragPoint);
    public abstract void EndDrag (Vector2 dropPoint);
}
