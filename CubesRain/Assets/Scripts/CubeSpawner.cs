using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private Cube _cubePrefab;
    [SerializeField] private Vector3 _minPosition;
    [SerializeField] private Vector3 _maxPosition;
    [SerializeField] private int _poolCapacity;
    [SerializeField] private int _poolMaxSize;
    [SerializeField] private float _repeatRate;

    private ObjectPool<Cube> _pool;

    private void Awake()
    {
        _pool = new ObjectPool<Cube>(
        createFunc: () => Instantiate(_cubePrefab, transform, true),
        actionOnGet: (cube) => ActionOnGet(cube),
        actionOnRelease: (cube) => ActionOnRelease(cube),
        actionOnDestroy: (cube) => Destroy(cube.gameObject),
        defaultCapacity: _poolCapacity,
        maxSize: _poolMaxSize);
    }

    private void OnEnable() => Cube.OnTouched += Release;

    private void OnDisable() => Cube.OnTouched -= Release;

    private void Start() => InvokeRepeating(nameof(GetCube), 0.0f, _repeatRate);

    private void GetCube() => _pool.Get();

    private void Release(Cube cube) => StartCoroutine(WaitForRelease(cube));

    private void ActionOnGet(Cube cube)
    {
        cube.transform.position = GetSpawnPosition();
        cube.gameObject.SetActive(true);
    }

    private void ActionOnRelease(Cube cube)
    {
        cube.gameObject.SetActive(false);
        cube.Reset();
    }
 
    private Vector3 GetSpawnPosition()
    {
        return new Vector3(Random.Range(_minPosition.x, _maxPosition.x),
                           Random.Range(_minPosition.y, _maxPosition.y),
                           Random.Range(_minPosition.z, _maxPosition.z));
    }

    private IEnumerator WaitForRelease(Cube cube)
    {
        int _minLifeTime = 2;
        int _maxLifeTime = 5;
        int delay = Random.Range(_minLifeTime, _maxLifeTime);

        yield return new WaitForSeconds(delay);

        _pool.Release(cube);
    }
}