using UnityEngine;

public class SelectInfo {
    public readonly IDraggable SelectedDraggableObject;
    public readonly RaycastHit2D RaycastHit;

    public SelectInfo (IDraggable selectedDraggableObject, RaycastHit2D raycastHit) {
        SelectedDraggableObject = selectedDraggableObject;
        RaycastHit = raycastHit;
    }
}