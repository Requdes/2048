using UnityEngine;
using Zenject;
using System;

public class DraggableObjectSelector : IDraggableObjectSelector, IInitializable, IDisposable {
    public event Action<SelectInfo> OnSelect;
    public event Action OnDeselect;

    private IPlayerInputHandler _inputHandler;
    private IRaycastService _raycastService;

    public DraggableObjectSelector (IPlayerInputHandler inputHandler, IRaycastService raycastService) {
        _inputHandler = inputHandler;
        _raycastService = raycastService;
    }

    public void Initialize () {
        _inputHandler.OnPointerDown += Select;
        _inputHandler.OnPointerUp += Deselect;
    }

    private void Select () {
        var hit = _raycastService.Raycast(Input.mousePosition);
         
        if (hit.transform == null) return;
        if (!hit.transform.TryGetComponent<IDraggable>(out var draggableObject)) return;
        if (draggableObject.IsDraggeble == false) return;

        var selectInfo = new SelectInfo(draggableObject, hit);

        OnSelect?.Invoke(selectInfo);
    }

    private void Deselect () {
        OnDeselect?.Invoke();
    }

    public void Dispose () {
        _inputHandler.OnPointerDown -= Select;
        _inputHandler.OnPointerUp -= Deselect;
    }
}