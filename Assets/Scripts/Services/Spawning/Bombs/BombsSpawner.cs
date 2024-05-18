using UnityEngine;

[RequireComponent(typeof(BombsReleaser))]
public class BombsSpawner : SpawnerBase<Bomb>
{
    public Vector3 SpawnPosition { get; set; }

    private BombsReleaser _bombsReleaser;
    
    private new void Awake()
    {
        base.Awake();

        _bombsReleaser = GetComponent<BombsReleaser>();
    }

    public Bomb Spawn()
    {
        return GetFromPool();
    }

    protected override void InitObject(Bomb bomb)
    {
        bomb.Init(_bombsReleaser);
    }
}