using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public sealed class CoroutineRunner : MonoBehaviour, ICoroutineRunner {
    private List<Coroutine> _routines = new();

    public Coroutine StartRoutine (IEnumerator enumerator) {
        var routine = StartCoroutine(enumerator);

        _routines.Add(routine);

        return routine;
    }

    public void StopRoutine (Coroutine routine) {
        if (routine == null) return;

        StopCoroutine(routine);

        _routines.Remove(routine);
    }
    
    public void StopAll () {
        foreach (var routine in _routines.ToArray()) StopRoutine(routine);
    }
}
