using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class Cube : MonoBehaviour
{
    private bool _isHitPlane;

    private void OnCollisionEnter(Collision collision)
    {
        if (!_isHitPlane)
        {
            if (collision.collider.TryGetComponent(out Plane plane) && collision.collider.TryGetComponent(out Releaser releaser))
            {
                _isHitPlane = true;

                var renderer = GetComponent<Renderer>();
                renderer.material.color = Random.ColorHSV();

                StartCoroutine(ReleaseWithDelay(releaser));
            }
        }
    }

    private IEnumerator ReleaseWithDelay(Releaser releaser)
    {
        var lifeTime = Random.Range(2, 5);

        yield return new WaitForSeconds(lifeTime);

        _isHitPlane = false;
        releaser.Release(this);
    }
}