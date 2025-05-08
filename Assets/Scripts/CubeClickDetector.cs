using System;
using UnityEngine;

public class CubeClickDetector : MonoBehaviour
{
    [SerializeField] private float _distance;

    public event Action<Cube> Detecting;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) == false)
            return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (IsHitCube(ray, out Cube cube))
            Detecting?.Invoke(cube);
    }

    private bool IsHitCube(Ray ray, out Cube cube)
    {
        cube = default;

        if (Physics.Raycast(ray, out RaycastHit hit, _distance) == false)
            return false;

        return hit.rigidbody.gameObject.TryGetComponent(out cube);
    }
}