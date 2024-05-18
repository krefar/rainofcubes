using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class Cube : MonoBehaviour
{
    private Renderer _renderer;
    private bool _isHitPlane;
    private int _releaseAfterSeconds;

    private void Awake()
    {
        _releaseAfterSeconds = Random.Range(2, 5);
        _renderer = GetComponent<Renderer>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!_isHitPlane)
        {
            if (collision.collider.TryGetComponent(out CubesReleaser releaser))
            {
                _isHitPlane = true;

                _renderer.material.color = Random.ColorHSV();

                StartCoroutine(ReleaseWithDelay(releaser));
            }
        }
    }

    public void Init(Vector3 position)
    {
        _isHitPlane = false;
        transform.position = position;
        gameObject.SetActive(true);
        _renderer.material.color = Color.white;
    }

    private IEnumerator ReleaseWithDelay(CubesReleaser releaser)
    {
        yield return new WaitForSeconds(_releaseAfterSeconds);

        releaser.Release(this);
    }
}