using System;

public interface IPlayerInputHandler {
    event Action OnPointerDown;
    event Action OnPointerUp;
}