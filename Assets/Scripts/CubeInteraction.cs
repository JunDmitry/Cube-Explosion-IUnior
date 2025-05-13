using System.Collections.Generic;
using UnityEngine;

public class CubeInteraction : MonoBehaviour
{
    [SerializeField] private CubeClickDetector _cubeDetector;
    [SerializeField] private Spawner _splitter;
    [SerializeField] private Explosion _explosion;

    private float _maxSplitChance = 1f;

    private void OnEnable()
    {
        _cubeDetector.Detecting += OnDetecting;
    }

    private void OnDisable()
    {
        _cubeDetector.Detecting += OnDetecting;
    }

    private void OnDetecting(Cube cube)
    {
        if (CanSplit(cube.SplitChance))
        {
            List<Cube> childs = _splitter.Split(cube);
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