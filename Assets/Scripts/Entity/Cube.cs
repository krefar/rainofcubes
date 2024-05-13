using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class Cube : MonoBehaviour
{
    private bool _hitPlane;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.TryGetComponent(out Plane plane) && collision.collider.TryGetComponent(out Releaser releaser))
        {
            if (!_hitPlane)
            {
                _hitPlane = true;
                
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

        _hitPlane = false;
        releaser.Release(this);
    }
}