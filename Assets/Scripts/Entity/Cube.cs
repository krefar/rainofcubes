using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class Cube : MonoBehaviour
{
    private Renderer _renderer;
    private bool _isHitPlane;

    private void Awake()
    {
       _renderer = GetComponent<Renderer>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!_isHitPlane)
        {
            if (collision.collider.TryGetComponent(out Plane plane) && collision.collider.TryGetComponent(out Releaser releaser))
            {
                _isHitPlane = true;

                _renderer.material.color = Random.ColorHSV();

                StartCoroutine(ReleaseWithDelay(releaser));
            }
        }
    }

    public void Init(Vector3 position)
    {
        transform.position = position;
        gameObject.SetActive(true);
        _renderer.material.color = Color.white;
    }

    private IEnumerator ReleaseWithDelay(Releaser releaser)
    {
        var lifeTime = Random.Range(2, 5);

        yield return new WaitForSeconds(lifeTime);

        _isHitPlane = false;
        releaser.Release(this);
    }
}