using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class AbstractCollisingObject : MonoBehaviour, ICollising {
    public event Action<ICollising, Collision2D> OnCollising;

    public IEnumerable<Collider2D> Colliders => _colliders;
    protected List<Collider2D> _colliders = new();
    
    public bool IsColliding => _isColliding;
    protected bool _isColliding = true;

    protected virtual void Awake() {
        _colliders.AddRange(GetComponentsInChildren<Collider2D>());
    }

    public virtual void ActivateColliding () {
        _isColliding = true;

        foreach (var collider in _colliders) collider.enabled = true;
    }

    public virtual void DeactivateColliding () {
        _isColliding = false;

        foreach (var collider in _colliders) collider.enabled = false;
    }

    protected virtual void OnCollisionEnter2D (Collision2D other) {
        if (_isColliding) OnCollising?.Invoke(this, other);
    }
}
