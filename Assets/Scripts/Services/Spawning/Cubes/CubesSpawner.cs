using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BombsSpawner))]
public class CubesSpawner : SpawnerBase<Cube>
{
    [SerializeField] private int _spawnPointCount = 100;
    [SerializeField] private float _repeatRate = 0.3f;

    private float _spawnPointY = 10.0f;
    private float _spawnPointMax = 5.0f;
    private float _spawnPointMin = -5.0f;

    private List<Vector3> _spawnPoints;
    
    private BombsSpawner _bombSpawner;
    
    private new void Awake()
    {
        base.Awake();

        _spawnPoints = GenerateSpawnPoints();
        _bombSpawner = GetComponent<BombsSpawner>();
    }

    private void Start()
    {
        StartCoroutine(SpawnCubes());
    }

    private void OnEnable()
    {
        OnRelease += SpawnBomb;
    }

    private void OnDisable()
    {
        OnRelease -= SpawnBomb;
    }

    protected override void InitObject(Cube cube)
    {
        cube.Init(GetSpawnPosition());
    }

    private List<Vector3> GenerateSpawnPoints()
    {
        var spawnPoints = new List<Vector3>();

        for (var i = 0; i < _spawnPointCount; i++)
        {
            var spawnPointX = Random.Range(_spawnPointMin, _spawnPointMax);
            var spawnPointZ = Random.Range(_spawnPointMin, _spawnPointMax);

            spawnPoints.Add(new Vector3(spawnPointX, _spawnPointY, spawnPointZ));
        }

        return spawnPoints;
    }

    private Vector3 GetSpawnPosition()
    {
        var randomIndex = Random.Range(0, _spawnPoints.Count);

        return _spawnPoints[randomIndex];
    }

    private IEnumerator SpawnCubes()
    {
        var wait = new WaitForSeconds(_repeatRate);

        while (enabled)
        {
            this.GetFromPool();

            yield return wait;
        }
    }

    private void SpawnBomb(Cube cube)
    {
        var bomb = _bombSpawner.Spawn();
        bomb.transform.position = cube.transform.position;
    }
}