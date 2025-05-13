using System.Collections;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private float _force;
    [SerializeField] private float _radius;

    [SerializeField] private ParticleSystem _effect;
    [SerializeField] private AudioSource _sourceClip;

    public void ExplodeAtPosition(Vector3 position, float multiplier = 1f)
    {
        Collider[] colliders = Physics.OverlapSphere(position, _radius * multiplier);

        foreach (Collider collider in colliders)
            if (collider.attachedRigidbody != null && collider.gameObject.TryGetComponent(out Cube _))
                ApplyKnockback(collider.attachedRigidbody, position, multiplier);

        PlayEffect(position);
    }

    public void ApplyKnockback(Rigidbody body, Vector3 position, float multiplier = 1f)
    {
        body.AddExplosionForce(_force * multiplier, position, _radius * multiplier);
    }

    private void PlayEffect(Vector3 position)
    {
        if (_effect == null)
        {
            Debug.LogWarning($"Effect {nameof(_effect)} is not set!");
            return;
        }

        ParticleSystem effect = Instantiate(_effect, position, Quaternion.identity);
        _sourceClip.transform.position = position;
        _sourceClip.Play();

        StartCoroutine(DestroyAfterStop(effect));
    }

    private IEnumerator DestroyAfterStop(ParticleSystem effect)
    {
        yield return new WaitWhile(() => effect.IsAlive() && enabled);

        if (effect != null)
            Destroy(effect.gameObject);
    }
}