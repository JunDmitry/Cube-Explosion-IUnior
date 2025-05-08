using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private Splitter _splitter;
    [SerializeField] private float _force;
    [SerializeField] private float _radius;

    private void OnEnable()
    {
        _splitter.CreatedChild += Explode;
    }

    private void OnDisable()
    {
        _splitter.CreatedChild -= Explode;
    }

    private void Explode(Cube cube, Vector3 parentPosition)
    {
        if (cube.gameObject.TryGetComponent(out Rigidbody rigidbody))
            rigidbody.AddExplosionForce(_force, parentPosition, _radius);
    }
}