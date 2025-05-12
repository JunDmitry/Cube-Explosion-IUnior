using UnityEngine;

public class Knockback : MonoBehaviour
{
    [SerializeField] private float _force;
    [SerializeField] private float _radius;

    public float Radius => _radius;

    public void Apply(Rigidbody unit, Vector3 position, float multiplier = 1f)
    {
        unit.AddExplosionForce(_force * multiplier, position, _radius * multiplier);
    }
}