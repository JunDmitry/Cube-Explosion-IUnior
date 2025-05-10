using System;
using UnityEngine;

public class CubeClickDetector : MonoBehaviour
{
    [SerializeField] private float _distance;
    [SerializeField] private InputHandler _handler;

    public event Action<Cube> Detecting;

    private void OnEnable()
    {
        _handler.ClickingMouseDown += OnClickingMouseDown;
    }

    private void OnDisable()
    {
        _handler.ClickingMouseDown -= OnClickingMouseDown;
    }

    private void OnClickingMouseDown()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (IsHitCube(ray, out Cube cube))
            Detecting?.Invoke(cube);
    }

    private bool IsHitCube(Ray ray, out Cube cube)
    {
        cube = default;

        if (Physics.Raycast(ray, out RaycastHit hit, _distance) == false)
            return false;

        return hit.collider.gameObject.TryGetComponent(out cube);
    }
}