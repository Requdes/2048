using UnityEngine;

public class RaycastService : IRaycastService {
    private const float CameraDistance = 8f;

    public Vector2 MousePositionToWorld => ScreenPointToWorld(Input.mousePosition);

    private Camera _camera;
    
    public RaycastService () {
        _camera = Camera.main;
    }

    public RaycastHit2D Raycast (Vector3 mousePosition) {
        var worldPoint = ScreenPointToWorld(mousePosition); 
        var hit = Physics2D.Raycast(worldPoint, Vector2.zero);
        
        return hit;
    }

    public Vector2 ScreenPointToWorld (Vector3 mousePosition) {
        var ray = _camera.ScreenPointToRay(mousePosition);
        var worldPoint = ray.GetPoint(CameraDistance);
    
        return worldPoint;
    }
}