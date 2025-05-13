using UnityEngine;

public class CubeFactory : MonoBehaviour
{
    [SerializeField] private Cube _prefab;
    [SerializeField] private Transform _container;
    [SerializeField] private Vector3[] _initialCubePositions;

    private void Awake()
    {
        if (_container == null)
            _container = transform;

        foreach (Vector3 position in _initialCubePositions)
            Create(position, _prefab.transform.localScale);
    }

    public Cube Create(Vector3 position, Vector3 scale)
    {
        Cube child = Instantiate(_prefab, position, Random.rotation);
        child.transform.SetParent(_container);
        child.transform.localScale = scale;
        child.Clicking += OnClicking;

        return child;
    }

    private void OnClicking(Cube cube)
    {
        Destroy(cube.gameObject);
    }
}