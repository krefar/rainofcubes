using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;
using Object = UnityEngine.Object;

public abstract class SpawnerBase<T> : MonoBehaviour
    where T : Object, new()
{
    [SerializeField] private T _prefab;

    public event Action<T> OnGetting;
    public event Action<T> OnRelease;

    private ObjectPool<T> _pool;

    protected void Awake()
    {
        _pool = new ObjectPool<T>(
            createFunc: () => CreateObject(),
            actionOnGet: (obj) => InitObject(obj),
            actionOnRelease: (obj) => obj.GameObject().SetActive(false),
            actionOnDestroy: (obj) => Destroy(obj.GameObject())
        );
    }

    public void Release(T obj)
    {
        _pool.Release(obj);

        this.OnRelease?.Invoke(obj);
    }

    public int GetActiveCount()
    {
        return _pool.CountActive;
    }

    public int GetAllCount()
    {
        return _pool.CountAll;
    }

    protected abstract void InitObject(T obj);

    protected T GetFromPool()
    {
        var obj = _pool.Get();

        this.OnGetting?.Invoke(obj);

        return obj;
    }

    private T CreateObject()
    {
        var obj = Instantiate(_prefab);
        InitObject(obj);

        return obj;
    }
}