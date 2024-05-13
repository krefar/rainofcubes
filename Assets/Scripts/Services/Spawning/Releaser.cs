using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using Random = UnityEngine.Random;

public class Releaser : MonoBehaviour
{
    [SerializeField] private Spawner _spawner;
    
    public void Release(Cube cube)
    {
        _spawner.Release(cube);
    }
}