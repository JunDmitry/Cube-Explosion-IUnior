using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private float _force;
    [SerializeField] private float _radius;

    public void Explode(Rigidbody rigidbody)
    {
        rigidbody.AddExplosionForce(_force, rigidbody.transform.position, _radius);
    }
}