using System.Collections;
using UnityEngine;

public class GameplayState : IExitableState {
    private ICoroutineRunner _coroutineRunner;
    private ISpawnerService _spawner;

    private Coroutine _spawnRoutine;

    public GameplayState (ICoroutineRunner coroutineService, ISpawnerService spawner) {
        _coroutineRunner = coroutineService;
        _spawner = spawner;
    }
    
    public void Enter () {
        _spawnRoutine = _coroutineRunner.StartRoutine(IntervalSpawn(6f));
    }

    private IEnumerator IntervalSpawn (float interval) {
        while (true) {
            yield return new WaitForSeconds(interval);

            _spawner.Spawn();
        }
    }

    public void Exit () {
        _coroutineRunner.StopRoutine(_spawnRoutine);
    }
}
