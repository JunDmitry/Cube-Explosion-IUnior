using System;
using UnityEngine;

public class Splitter : MonoBehaviour
{
    private const float MinMultiplier = 0.00001f;

    [SerializeField] private Spawner _spawner;
    [SerializeField] private Knockback _knockback;

    [SerializeField, Min(0)] private int _minSplitCount = 0;
    [SerializeField] private int _maxSplitCount;
    [SerializeField, Min(MinMultiplier)] private float _scaleMultiplier = MinMultiplier;
    [SerializeField, Min(MinMultiplier)] private float _splitMultiplier = MinMultiplier;

    public event Action<Cube> CreatingChild;

    public void Split(Cube cube)
    {
        int childCount = UnityEngine.Random.Range(_minSplitCount, _maxSplitCount + 1);
        Transform parent = cube.transform;
        Vector3 childScale = parent.localScale * _scaleMultiplier;
        Cube child;

        for (int i = 0; i < childCount; i++)
        {
            child = _spawner.Spawn(parent.position, childScale);
            child.Initialize(cube.SplitChance * _splitMultiplier);
            CreatingChild?.Invoke(child);

            if (child.gameObject.TryGetComponent(out Rigidbody body))
                _knockback.Apply(body, parent.position);
        }
    }

    private void OnValidate()
    {
        if (_maxSplitCount < _minSplitCount)
            _maxSplitCount = _minSplitCount;
    }
}