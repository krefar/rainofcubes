using System.Collections;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] float _radius = 1f;
    [SerializeField] float _explosionSpeed = 1f;

    private void Start()
    {
        StartCoroutine(Explode());
    }

    private IEnumerator Explode()
    {
        var wait = new WaitForEndOfFrame();

        while (transform.localScale.x < Vector3.one.x * _radius)
        {
            transform.localScale += Vector3.one * _explosionSpeed * Time.deltaTime;

            yield return wait;
        }

        Destroy(gameObject);
    }
}