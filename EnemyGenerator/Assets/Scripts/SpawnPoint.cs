using UnityEngine;
using UnityEngine.Pool;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private Unit[] _units;
    [SerializeField] private Directions _directions;
    [SerializeField] private int _poolCapacity = 3;
    [SerializeField] private int _poolMaxSize = 3;

    private ObjectPool<Unit> _pool;

    private void Awake()
    {
        _pool = new ObjectPool<Unit>(
        createFunc: () => Instantiate(_units[Random.Range(0, _units.Length)], transform, true),
        actionOnGet: (unit) => ActionOnGet(unit),
        actionOnRelease: (unit) => ActionOnRelease(unit),
        actionOnDestroy: (unit) => Destroy(unit.gameObject),
        defaultCapacity: _poolCapacity,
        maxSize: _poolMaxSize);
    }

    public void CreateUnit() => _pool.Get();

    private void Release(Unit unit) => _pool.Release(unit);

    private void ActionOnGet(Unit unit)
    {
        unit.transform.position = transform.position;
        unit.SetDirection(GetDirection());
        unit.gameObject.SetActive(true);
        unit.OnReleased += Release;
    }

    private void ActionOnRelease(Unit unit)
    {
        unit.gameObject.SetActive(false);
        unit.OnReleased -= Release;
    }        

    private Vector2 GetDirection()
    {
        return _directions switch
        {
            Directions.Right => Vector2.right,
            Directions.Left => Vector2.left,
            Directions.Top => Vector2.up,
            Directions.Bottom => Vector2.down,
            _ => Vector2.zero,
        };
    }
}