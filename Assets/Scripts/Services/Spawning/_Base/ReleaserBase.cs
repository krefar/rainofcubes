using UnityEngine;

public abstract class ReleaserBase<T> : MonoBehaviour
    where T : Object, new()
{
    [SerializeField] private SpawnerBase<T> _spawner;
    
    public void Release(T obj)
    {
        _spawner.Release(obj);
    }
}