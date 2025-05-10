using System;
using System.Collections.Generic;
using UnityEngine;

public class Splitter : MonoBehaviour
{
    private const float MinMultiplier = 0.00001f;

    [SerializeField] private Cube _prefab;
    [SerializeField] private Transform _container;

    [SerializeField, Min(0)] private int _minSplitCount = 0;
    [SerializeField] private int _maxSplitCount;
    [SerializeField, Min(MinMultiplier)] private float _scaleMultiplier = MinMultiplier;
    [SerializeField, Min(MinMultiplier)] private float _splitMultiplier = MinMultiplier;

    public event Action<Cube> CreatedChild;

    public List<Cube> Split(Cube cube)
    {
        List<Cube> childs = new();
        int childCount = UnityEngine.Random.Range(_minSplitCount, _maxSplitCount + 1);
        Transform parent = cube.transform;
        Vector3 childScale = parent.localScale * _scaleMultiplier;
        Cube child;

        for (int i = 0; i < childCount; i++)
        {
            child = Spawn(parent.position, childScale, cube.SplitChance);
            childs.Add(child);
        }

        return childs;
    }

    private Cube Spawn(Vector3 position, Vector3 scale, float parentSplitChance)
    {
        Cube child = Instantiate(_prefab, position, UnityEngine.Random.rotation);
        child.transform.SetParent(_container);
        child.transform.localScale = scale;
        child.Clicking += OnClicking;
        child.Initialize(parentSplitChance * _splitMultiplier);

        CreatedChild?.Invoke(child);

        return child;
    }

    private void Awake()
    {
        if (_container != null && _container.gameObject.TryGetComponent(out CubeInitializer initializer))
            initializer.Initialize(OnClicking);
    }

    private void OnValidate()
    {
        if (_maxSplitCount < _minSplitCount)
            _maxSplitCount = _minSplitCount;
    }

    private void OnClicking(Cube cube)
    {
        Destroy(cube.gameObject);
    }
}