using System.Collections;
using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    [SerializeField] private float _smoothDuration;

    public void Change(Cube cube)
    {
        if (cube.TryGetComponent(out Renderer renderer))
            StartCoroutine(ChangeColorSmoothly(renderer.material, Random.ColorHSV()));
        else
            Debug.LogWarning("Cannot change color Gameobject without Renderer component!");
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