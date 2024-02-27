using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using DG.Tweening;
using System.Linq;
using Zenject;
using System;

public class Merger : IInitializable, IDisposable {
    private const float ApproachDurationToCenter = 0.25f;
    private const float CompressionDuration = 0.3f;
    private const float CompressionSize = 0.3f;
    private const float DurationExpansion = 0.3f;
    private const float AlignDuration = 0.3f;
    private const float RoundingDuration = 0.3f;
    private const float DurationColorChange = 0.3f;

    private IMergingCubeFactoryService _factory;
    private ICoroutineRunner _coroutineRunner;
    private ICubeStylistService _stylistService;
    private ICollisingDetector<BaseMergingCube, BaseMergingCube> _collisionDetector;

    private Dictionary<MergerData, Coroutine> _mergerCoroutineMap = new();

    public Merger (
        IMergingCubeFactoryService factory, 
        ICoroutineRunner coroutineRunner,
        ICubeStylistService stylistService,
        ICollisingDetector<BaseMergingCube, BaseMergingCube> collisionDetector) {
        
        _factory = factory;
        _coroutineRunner = coroutineRunner;
        _stylistService = stylistService;
        _collisionDetector = collisionDetector;
    }

    public void Initialize () {
        _collisionDetector.OnCollising += StartMerge;
    }

    private void StartMerge (BaseMergingCube cubeA, BaseMergingCube cubeB) {
        if (!IsMergeable(cubeA, cubeB)) return;

        var mergedCube = CreateMergedCube(cubeA, cubeB);
        var mergerData = new MergerData (cubeA, cubeB, mergedCube);

        PrepareMerge(mergerData);

        var mergingCoroutine = _coroutineRunner.StartRoutine(DoMerge(mergerData));
        _mergerCoroutineMap.Add(mergerData, mergingCoroutine);
    }

    private bool IsMergeable (BaseMergingCube cubeA, BaseMergingCube cubeB) {
        return cubeA.MergingIndex == cubeB.MergingIndex;
    } 

    private BaseMergingCube CreateMergedCube (BaseMergingCube cubeA, BaseMergingCube cubeB) {
        var mergeCenter  = cubeB.transform.position + (cubeA.transform.position - cubeB.transform.position) / 2;
        var mergedCube = _factory.CreateImproved(cubeA);

        mergedCube.transform.position = mergeCenter;
        mergedCube.SetScale(Vector3.one * CompressionSize);
        mergedCube.Round();

        return mergedCube;
    }

    private void PrepareMerge (MergerData mergerData) {
        // Stop the merge animation if it is still running
        StopMerging(mergerData.CubeA);
        StopMerging(mergerData.CubeB);
    }

    private void StopMerging (BaseMergingCube cube) {
        var mergerData = _mergerCoroutineMap.Keys.Where(mergerData => mergerData.MergedCube == cube).FirstOrDefault();

        if (mergerData == null) return;

        _coroutineRunner.StopRoutine(_mergerCoroutineMap[mergerData]);
        MergeEnd(mergerData);
    }

    private IEnumerator DoMerge (MergerData mergerData) {
        MergeCubes(mergerData);

        yield return new WaitForSeconds(0.3f);

        ExpandMergedCube(mergerData.MergedCube);

        MergeEnd(mergerData);
    }

    private void MergeCubes (MergerData mergerData) {
        MergeCube(mergerData.CubeA, mergerData.MergedCube);
        MergeCube(mergerData.CubeB, mergerData.MergedCube);
    }

    private void MergeCube (BaseMergingCube cube, BaseMergingCube mergedCube) {
        var compressionSize = Vector3.one * CompressionSize;
        var color = _stylistService.GetStyle(mergedCube.MergingIndex).Material.color;

        cube.transform.SetParent(mergedCube.transform, true);
        cube.transform.DOLocalMove(Vector3.zero, ApproachDurationToCenter).SetEase(Ease.InQuad).SetLink(cube.gameObject);
        cube.DoScale(compressionSize, CompressionDuration);
        cube.DoChangeColor(color, DurationColorChange);
        cube.DoRound(RoundingDuration);

        cube.DeactivatePhysics();
        cube.DeactivateColliding();
        cube.DeactivateDraggable();
    }

    private void ExpandMergedCube (BaseMergingCube mergedCube) {
        mergedCube.DoScale(Vector3.one, DurationExpansion);
        mergedCube.DoAlign(AlignDuration);
    }

    private void MergeEnd (MergerData mergerData) {
        _factory.Remove(mergerData.CubeA);
        _factory.Remove(mergerData.CubeB);
        _mergerCoroutineMap.Remove(mergerData);
    }

    public void Dispose () {
        _collisionDetector.OnCollising -= StartMerge;
    }

    private class MergerData {
        public BaseMergingCube CubeA;
        public BaseMergingCube CubeB;
        public BaseMergingCube MergedCube;

        public MergerData (BaseMergingCube cubeA, BaseMergingCube cubeB, BaseMergingCube mergedCube) {
            CubeA = cubeA;
            CubeB = cubeB;
            MergedCube = mergedCube;
        }
    }
}
