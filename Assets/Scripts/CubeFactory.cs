using UnityEngine;

public class CubeFactory : MonoBehaviour
{
    [SerializeField] private Cube _prefab;
    [SerializeField] private Transform _container;

    public Cube Create(Vector3 position, Vector3 scale)
    {
        Cube child = Instantiate(_prefab, position, Random.rotation);
        child.transform.SetParent(_container);
        child.transform.localScale = scale;
        child.Clicking += OnClicking;

        return child;
    }

    private void Awake()
    {
        if (_container != null && _container.gameObject.TryGetComponent(out CubeInitializer initializer))
            initializer.Initialize(OnClicking);
    }

    private void OnClicking(Cube cube)
    {
        Destroy(cube.gameObject);
    }
}