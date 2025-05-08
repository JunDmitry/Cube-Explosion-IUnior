using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeInteraction : MonoBehaviour
{
    [SerializeField] private CubeClickDetector _cubeDetector;
    [SerializeField] private Splitter _splitter;
    [SerializeField] private Explosion _explosion;

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
        if (IsSplit(cube.SplitChance))
        {
            List<Cube> childs = _splitter.Split(cube);
            ExplodeAll(childs);
        }

        Destroy(cube.gameObject);
    }

    private bool IsSplit(float chance)
    {
        return chance >= Random.value;
    }

    private void ExplodeAll(IEnumerable<Cube> cubes)
    {
        foreach (Cube cube in cubes)
            Explode(cube);
    }

    private void Explode(Cube cube)
    {
        if (cube.gameObject.TryGetComponent(out Rigidbody rigidbody))
            _explosion.Explode(rigidbody);
    }
}