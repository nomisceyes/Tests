using UnityEngine;

public class Spawn : MonoBehaviour
{
    [SerializeField] private SpawnPoint[] _spawnPoints;
    [SerializeField] private float _repeatRate = 1.0f;

    private void Start()
    {
        if (_spawnPoints.Length > 0)
            InvokeRepeating(nameof(SpawnUnit), 0.0f, _repeatRate);
    }

    private void SpawnUnit()
    {
        int number = Random.Range(0, _spawnPoints.Length);

        _spawnPoints[number].CreateUnit();
    }
}