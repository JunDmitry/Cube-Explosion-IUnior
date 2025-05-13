using System.Collections.Generic;
using UnityEngine;

public class CubeEventHub : MonoBehaviour
{
    [SerializeField] private CubeClickDetector _cubeDetector;
    [SerializeField] private Spawner _spawner;
    [SerializeField] private Explosion _explosion;
    [SerializeField] private ColorChanger _colorChanger;

    private float _maxSplitChance = 1f;

    private void OnEnable()
    {
        _cubeDetector.Detecting += OnDetecting;
        _spawner.CreatingChild += _colorChanger.Change;
    }

    private void OnDisable()
    {
        _cubeDetector.Detecting -= OnDetecting;
        _spawner.CreatingChild -= _colorChanger.Change;
    }

    private void OnDetecting(Cube cube)
    {
        if (CanSplit(cube.SplitChance))
        {
            List<Cube> childs = _spawner.Split(cube);
            KnockbackAll(childs, cube.transform.position);
        }
        else
        {
            _explosion.ExplodeAtPosition(cube.transform.position, _maxSplitChance / cube.SplitChance);
        }

        cube.InteractAfterClick();
    }

    private bool CanSplit(float chance)
    {
        return chance >= Random.value;
    }

    private void KnockbackAll(IEnumerable<Cube> childs, Vector3 position)
    {
        foreach (Cube child in childs)
            if (child.gameObject.TryGetComponent(out Rigidbody body))
                _explosion.ApplyKnockback(body, position);
    }
}