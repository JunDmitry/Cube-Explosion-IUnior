using UnityEngine;

public class CubeInteraction : MonoBehaviour
{
    [SerializeField] private CubeClickDetector _cubeDetector;
    [SerializeField] private Splitter _splitter;
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
            _splitter.Split(cube);
        else
            _explosion.ExplodeAtPosition(cube.transform.position, _maxSplitChance / cube.SplitChance);

        cube.InteractAfterClick();
    }

    private bool CanSplit(float chance)
    {
        return chance >= Random.value;
    }
}