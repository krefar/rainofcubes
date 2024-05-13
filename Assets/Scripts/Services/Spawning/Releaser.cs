using UnityEngine;

public class Releaser : MonoBehaviour
{
    [SerializeField] private Spawner _spawner;
    
    public void Release(Cube cube)
    {
        _spawner.Release(cube);
    }
}