using UnityEngine;
using System;

public interface ICollisingDetector<CollidingObject, CollisionObject> where CollidingObject : ICollising where CollisionObject : Component, ICollising {
    event Action<CollidingObject, CollisionObject> OnCollising;
}
