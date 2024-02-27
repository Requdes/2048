using System.Collections.Generic;
using UnityEngine;
using System;

public interface ICollising {
    event Action<ICollising, Collision2D> OnCollising;
    
    IEnumerable<Collider2D> Colliders { get; }

    bool IsColliding { get; }

    void ActivateColliding ();
    void DeactivateColliding ();
}
