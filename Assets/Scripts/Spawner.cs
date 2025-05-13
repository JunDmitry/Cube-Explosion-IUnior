using System;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private const float MinMultiplier = 0.00001f;

    [SerializeField] private CubeFactory _factory;

    [SerializeField, Min(0)] private int _minSplitCount = 0;
    [SerializeField] private int _maxSplitCount;
    [SerializeField, Min(MinMultiplier)] private float _scaleMultiplier = MinMultiplier;
    [SerializeField, Min(MinMultiplier)] private float _splitMultiplier = MinMultiplier;

    public event Action<Cube> CreatingChild;

    public List<Cube> Split(Cube cube)
    {
        List<Cube> cubes = new();
        int childCount = UnityEngine.Random.Range(_minSplitCount, _maxSplitCount + 1);
        Transform parent = cube.transform;
        Vector3 childScale = parent.localScale * _scaleMultiplier;
        Cube child;

        for (int i = 0; i < childCount; i++)
        {
            child = _factory.Create(parent.position, childScale);
            child.Initialize(cube.SplitChance * _splitMultiplier);
            CreatingChild?.Invoke(child);

            cubes.Add(child);
        }

        return cubes;
    }

    private void OnValidate()
    {
        if (_maxSplitCount < _minSplitCount)
            _maxSplitCount = _minSplitCount;
    }
}