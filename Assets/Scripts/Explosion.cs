using System.Collections;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private Knockback _knockback;
    [SerializeField] private ParticleSystem _effect;

    public void ExplodeAtPosition(Vector3 position, float multiplier = 1f)
    {
        Collider[] colliders = Physics.OverlapSphere(position, _knockback.Radius * multiplier);

        foreach (Collider collider in colliders)
            if (collider.attachedRigidbody != null && collider.gameObject.TryGetComponent(out Cube _))
                _knockback.Apply(collider.attachedRigidbody, position, multiplier);

        PlayEffect(position);
    }

    private void PlayEffect(Vector3 position)
    {
        if (_effect == null)
        {
            Debug.LogWarning($"Effect {nameof(_effect)} is not set!");
            return;
        }

        ParticleSystem effect = Instantiate(_effect, position, Quaternion.identity);
        StartCoroutine(DestroyAfterStop(effect));
    }

    private IEnumerator DestroyAfterStop(ParticleSystem effect)
    {
        yield return new WaitWhile(() => effect.IsAlive() && enabled);

        if (effect != null)
            Destroy(effect.gameObject);
    }
}