using System;
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

    public event Action<Cube, Vector3> CreatedChild;

    public void Split(Cube cube)
    {
        int childCount = UnityEngine.Random.Range(_minSplitCount, _maxSplitCount + 1);
        Transform parent = cube.transform;
        Vector3 childScale = parent.localScale * _scaleMultiplier;
        Cube child;

        for (int i = 0; i < childCount; i++)
        {
            child = CreateChild(parent.position, childScale, cube.SplitChance);
            CreatedChild?.Invoke(child, parent.position);
        }
    }

    private Cube CreateChild(Vector3 position, Vector3 scale, float parentSplitChance)
    {
        Cube child = Instantiate(_prefab, position, UnityEngine.Random.rotation);
        child.transform.SetParent(_container);
        child.transform.localScale = scale;
        child.Initialize(this, parentSplitChance * _splitMultiplier);

        return child;
    }

    private void OnValidate()
    {
        if (_maxSplitCount < _minSplitCount)
            _maxSplitCount = _minSplitCount;
    }
}