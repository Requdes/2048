using UnityEngine;
using System;
using UniRx;

public interface ICollisingObjectRegistry {
    event Action<ICollising, Collision2D> OnAnyCollising;

    IReadOnlyReactiveCollection<ICollising> CollisingObjects { get; }

    void Add (ICollising collisingObject);
    void Remove (ICollising collisingObject);

    bool TryGetByCollider (Collider2D collider, out ICollising collisingObject);
}
