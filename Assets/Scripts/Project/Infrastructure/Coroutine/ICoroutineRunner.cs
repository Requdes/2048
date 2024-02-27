using System.Collections;
using UnityEngine;

public interface ICoroutineRunner {
    Coroutine StartRoutine (IEnumerator enumerator);
    void StopRoutine (Coroutine routine);
    void StopAll ();
}