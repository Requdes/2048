using UnityEngine;
using Zenject;
using System;

public class CollisionDetector<CollidingObject, CollisionObject> : ICollisingDetector<CollidingObject, CollisionObject>, IInitializable, IDisposable
    where CollidingObject : ICollising where CollisionObject : Component, ICollising {

    public event Action<CollidingObject, CollisionObject> OnCollising;

    private ICollisingObjectRegistry _collisingRegistry;

    public CollisionDetector (ICollisingObjectRegistry collisingRegistry) {
        _collisingRegistry = collisingRegistry;
    }

    public void Initialize () {
        _collisingRegistry.OnAnyCollising += Handle;
    }

    private void Handle (ICollising collisingObject, Collision2D collision) {
        if (!_collisingRegistry.TryGetByCollider(collision.collider, out var collisionObject)) return;

        if (collisingObject is not CollidingObject) return;
        if (collisionObject is not CollisionObject) return;

        OnCollising?.Invoke((CollidingObject)collisingObject, (CollisionObject)collisionObject);
    }

    public void Dispose () {
        _collisingRegistry.OnAnyCollising -= Handle;
    }
}