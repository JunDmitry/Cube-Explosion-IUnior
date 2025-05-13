using System.Collections;
using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    [SerializeField] private float _smoothDuration;
    [SerializeField] private Spawner _splitter;

    private void OnEnable()
    {
        _splitter.CreatingChild += OnCreatingChild;
    }

    private void OnDisable()
    {
        _splitter.CreatingChild -= OnCreatingChild;
    }

    private void OnCreatingChild(Cube cube)
    {
        if (cube.TryGetComponent(out Renderer renderer))
            StartCoroutine(ChangeColorSmoothly(renderer.material, Random.ColorHSV()));
    }

    private IEnumerator ChangeColorSmoothly(Material material, Color target)
    {
        Color source = material.color;
        float elapsedTime = 0f;

        while (elapsedTime < _smoothDuration && enabled)
        {
            elapsedTime += Time.deltaTime;

            material.color = Color.Lerp(source, target, elapsedTime / _smoothDuration);
            yield return null;
        }
    }
}