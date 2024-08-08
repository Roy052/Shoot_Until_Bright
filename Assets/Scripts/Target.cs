using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public ParticleSystem particle;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Bullet tempBullet = collision.GetComponent<Bullet>();
        if (tempBullet != null)
            tempBullet.isHit = true;

        Destroy(collision.gameObject);
        Singleton.audioManager.PlaySFX(SFX.Broke);
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
