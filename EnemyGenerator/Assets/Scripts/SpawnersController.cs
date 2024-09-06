using System.Collections;
using UnityEngine;

public class SpawnersController : MonoBehaviour
{
    [SerializeField] private Spawner[] _spawnPoints;
    [SerializeField] private float _repeatRate = 2.0f;

    readonly private bool _isWorking = true;

    private void Start()
    {
        if (_spawnPoints.Length > 0)
            StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        while (_isWorking)
        {
            _spawnPoints[Random.Range(0, _spawnPoints.Length)].CreateUnit();
            yield return new WaitForSeconds(_repeatRate);
        }
    }
}