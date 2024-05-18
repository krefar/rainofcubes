using TMPro;
using UnityEngine;
using Object = UnityEngine.Object;

public abstract class SpawnerView<T> : MonoBehaviour
    where T : Object, new()
{
    [SerializeField] private SpawnerBase<T> _spawner;
    [SerializeField] private TextMeshProUGUI _activeCountText;
    [SerializeField] private TextMeshProUGUI _allCountText;

    private void OnEnable()
    {
        _spawner.OnGetting += UpdateSpawnerInfo;   
    }

    private void OnDisable()
    {
        _spawner.OnGetting -= UpdateSpawnerInfo;
    }

    private void UpdateSpawnerInfo(T obj)
    {
        _activeCountText.text = $"Активных: {_spawner.GetActiveCount()}";
        _allCountText.text = $"Всего: {_spawner.GetAllCount()}";
    }
}