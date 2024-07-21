using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public ParticleSystem particle;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(collision.gameObject);
        StartCoroutine(OnHit());
    }

    IEnumerator OnHit()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        particle.Play();
        yield return new WaitForSeconds(2f);
        Singleton.objectSpawner.DestroyTarget(this);
        Destroy(gameObject);
    }
}
