using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Cube _cubePrefab;
    [SerializeField] private float _repeatRate;
    [SerializeField] private int _spawnPointCount = 20;

    private float _spawnPointHeight = 10.0f;
    private float _spawnPointMax = 5.0f;
    private float _spawnPointMin = -5.0f;
    private List<Vector3> _spawnPoints;
    private ObjectPool<Cube> _pool;

    private void Awake()
    {
        GenerateSpawnPoints();

        _pool = new ObjectPool<Cube>(
            createFunc: () =>
            {
                var cube = Instantiate(_cubePrefab);
                cube.transform.position = GetRandomPosition();

                return cube;
            },
            actionOnGet: (cube) => InitCube(cube),
            actionOnRelease: (cube) => cube.gameObject.SetActive(false),
            actionOnDestroy: (cube) => Destroy(cube.gameObject)
            );
    }

    private void Start()
    {
        StartCoroutine(SpawnCubes());
    }

    public void Release(Cube cube)
    {
        _pool.Release(cube);
    }

    private void GenerateSpawnPoints()
    {
        _spawnPoints = new List<Vector3>();

        for (var i = 0; i < _spawnPointCount; i++)
        {
            var x = Random.Range(_spawnPointMin, _spawnPointMax);
            var y = _spawnPointHeight;
            var z = Random.Range(_spawnPointMin, _spawnPointMax);

            _spawnPoints.Add(new Vector3(x, y, z));
        }
    }

    private IEnumerator SpawnCubes()
    {
        while (enabled)
        {
            _pool.Get();

            yield return new WaitForSeconds(_repeatRate);
        }
    }

    private void InitCube(Cube cube)
    {
        cube.Init(GetRandomPosition());
    }

    private Vector3 GetRandomPosition()
    {
        var randomIndex = Random.Range(0, _spawnPoints.Count);

        return _spawnPoints[randomIndex];
    }
}