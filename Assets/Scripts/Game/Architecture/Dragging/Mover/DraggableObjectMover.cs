using System.Collections;
using UnityEngine;
using Zenject;
using System;

public class DraggableObjectMover : IInitializable, IDisposable {
    private IDraggableObjectSelector _selector;
    private IRaycastService _raycastService;
    private ICoroutineRunner _coroutineRunner;

    private IDraggable _selectedDraggableObject;
    private Coroutine _draggingCoroutine;

    public DraggableObjectMover (IDraggableObjectSelector selector, IRaycastService raycastService, ICoroutineRunner coroutineRunner) {
        _selector = selector;
        _raycastService = raycastService;
        _coroutineRunner = coroutineRunner;
    }

    public void Initialize () {
        _selector.OnSelect += StartDragging;
        _selector.OnDeselect += EndDragging;
    }

    private void StartDragging (SelectInfo selectInfo) {
        _selectedDraggableObject = selectInfo.SelectedDraggableObject;
        _selectedDraggableObject.StartDrag(_raycastService.MousePositionToWorld);

        _draggingCoroutine = _coroutineRunner.StartRoutine(Dragging());
    }

    private IEnumerator Dragging () {
        while (true) {
            if (_selectedDraggableObject.IsDraggeble == false) EndDragging();

            _selectedDraggableObject?.Drag(_raycastService.MousePositionToWorld);

            yield return new WaitForEndOfFrame();
        }
    }

    private void EndDragging () {
        if (_selectedDraggableObject == null) return;

        _coroutineRunner.StopRoutine(_draggingCoroutine);
        _selectedDraggableObject.EndDrag(_raycastService.MousePositionToWorld);
        
        _draggingCoroutine = null;
        _selectedDraggableObject = null;
    }

    public void Dispose () {
        _selector.OnSelect -= StartDragging;
        _selector.OnDeselect -= EndDragging;
    }
}
