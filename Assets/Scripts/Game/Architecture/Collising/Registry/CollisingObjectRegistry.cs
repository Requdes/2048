using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using UniRx;

public class CollisingObjectRegistry : ICollisingObjectRegistry {
    public event Action<ICollising, Collision2D> OnAnyCollising;

    public IReadOnlyReactiveCollection<ICollising> CollisingObjects => _collisingObjects;
    private ReactiveCollection<ICollising> _collisingObjects = new();
    
    private Dictionary<Collider2D, ICollising> _collidersMap = new();
    
    public void Add (ICollising collisingObject) {
        _collisingObjects.Add(collisingObject);

        collisingObject.OnCollising += Collising;

        foreach (var collider in collisingObject.Colliders) _collidersMap.Add(collider, collisingObject);
    }

    public void Remove (ICollising collisingObject) {
        _collisingObjects.Remove(collisingObject);

        collisingObject.OnCollising -= Collising;

        var colliders = _collidersMap.Where(pair => pair.Value == collisingObject).Select(pair => pair.Key).ToArray();
        foreach (var collider in colliders) _collidersMap.Remove(collider);
    }

    public bool TryGetByCollider (Collider2D collider, out ICollising collisingObject) {
        return _collidersMap.TryGetValue(collider, out collisingObject);
    }

    private void Collising (ICollising collisingObject, Collision2D collision) {
        OnAnyCollising?.Invoke(collisingObject, collision);
    }
}
