using UnityEngine;

public interface IRaycastService {
    Vector2 MousePositionToWorld { get; }

    RaycastHit2D Raycast (Vector3 mousePosition);
    Vector2 ScreenPointToWorld (Vector3 mousePosition);
}