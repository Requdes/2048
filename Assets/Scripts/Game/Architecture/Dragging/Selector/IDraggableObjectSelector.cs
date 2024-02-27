using System;

public interface IDraggableObjectSelector {
    event Action<SelectInfo> OnSelect;
    event Action OnDeselect;
}
