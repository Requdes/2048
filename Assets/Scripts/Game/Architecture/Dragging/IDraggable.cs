using UnityEngine;

public interface IDraggable {
    bool IsDraggeble { get; }

    void ActivateDraggable ();
    void DeactivateDraggable ();

    void StartDrag (Vector2 startPoint);
    void Drag (Vector2 dragPoint);
    void EndDrag (Vector2 dropPoint);
}
