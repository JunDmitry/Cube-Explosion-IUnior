using UnityEngine;

public class Mover : MonoBehaviour
{
    private const string Horizontal = "Horizontal";
    private const string Vertical = "Vertical";

    [SerializeField] private float _speed;

    private float _horizontalDirection;
    private float _verticalDirection;

    private void Update()
    {
        _horizontalDirection = Input.GetAxis(Horizontal);
        _verticalDirection = Input.GetAxis(Vertical);
    }

    private void FixedUpdate()
    {
        if (Mathf.Abs(_horizontalDirection) <= 1e-9 && Mathf.Abs(_verticalDirection) <= 1e-9)
            return;

        Vector3 direction = new(_horizontalDirection, 0f, _verticalDirection);

        transform.Translate(_speed * Time.deltaTime * direction);
    }
}