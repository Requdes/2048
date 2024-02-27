using UnityEngine.InputSystem;
using Zenject;
using System;

public class PlayerInputHandler : IPlayerInputHandler, IInitializable, IDisposable {
    public event Action OnPointerDown;
    public event Action OnPointerUp;
    
    private PlayerInputAssets _inputAssets;

    public PlayerInputHandler () {
        _inputAssets = new PlayerInputAssets ();
        _inputAssets.Enable();
    }

    public void Initialize () {
        _inputAssets.Player.Press.performed += PointerDown;
        _inputAssets.Player.Press.canceled += PointerUp; 
    }

    private void PointerDown (InputAction.CallbackContext context) {
        OnPointerDown?.Invoke();
    }

    private void PointerUp (InputAction.CallbackContext context) {
        OnPointerUp?.Invoke();
    }

    public void Dispose () {
        _inputAssets.Player.Press.performed -= PointerDown;
        _inputAssets.Player.Press.canceled -= PointerUp; 
    }
}
