using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class Bomb : MonoBehaviour
{
    [SerializeField] Explosion _explosion;

    private Renderer _renderer;
    private int _releaseAfterSeconds;
    private float _lifeTime;

    private void Awake()
    {
        _releaseAfterSeconds = Random.Range(2, 5);
        _renderer = GetComponent<Renderer>();
    }

    private void Update()
    {
        _lifeTime += Time.deltaTime;
    }

    public void Init(BombsReleaser bombsReleaser)
    {
        _lifeTime = 0;
        _renderer.material.color = Color.black;
        gameObject.SetActive(true);

        StartCoroutine(Fade());
        StartCoroutine(ReleaseWithDelay(bombsReleaser));
    }

    private IEnumerator Fade()
    {
        var wait = new WaitForEndOfFrame();
        var currentColor = _renderer.material.color;

        while (_renderer.material.color.a > 0)
        {
            _renderer.material.color = Color.Lerp(currentColor, new Color(), Mathf.Clamp(_lifeTime, 0, _releaseAfterSeconds) / _releaseAfterSeconds);

            yield return wait;
        }
    }

    private IEnumerator ReleaseWithDelay(BombsReleaser releaser)
    {
        yield return new WaitForSeconds(_releaseAfterSeconds);

        releaser.Release(this);
        var explosion = Instantiate(_explosion);
        explosion.transform.position = transform.position;
    }
}