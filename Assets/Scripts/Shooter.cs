using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shooter : Singleton
{
    const float DistInventory = 180f;
    const float Speed = 0.1f;

    static Vector2 Center = new Vector2(0, Screen.height/2);

    public int bulletCount = 1;
    public GameObject objBullet;

    public Transform transformShooter;
    public Transform transformGun;

    private void Awake()
    {
        shooter = this;
    }

    private void Update()
    {
        if (gm.gameState != GameState.BeforeStart && gm.gameState != GameState.Aiming)
            return;

        Vector2 mousePos = Input.mousePosition;
        bool inInventoryCircle = Vector2.Distance(mousePos, Center) <= DistInventory;
        //Debug.Log(Vector2.Distance(mousePos, Center));
        if (inInventoryCircle)
            Debug.Log("In Inventory Circle");

        Vector2 dir = (mousePos - Center).normalized;
        transformGun.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(dir.y, dir.x) * 180 / Mathf.PI);

        if (Input.GetMouseButtonUp(0))
        {
            if (inInventoryCircle == false)
                Shoot(dir);
            else
                OpenInventory();
        }
    }

    void OpenInventory()
    {

    }

    void Shoot(Vector2 dir)
    {
        if (gm.gameState == GameState.Aiming)
            bulletCount--;

        GameObject temp = Instantiate(objBullet, objBullet.transform.position, Quaternion.Euler(0, 0, Mathf.Atan2(dir.y, dir.x) * 180 / Mathf.PI - 90f), objBullet.transform.parent);
        temp.SetActive(true);
        temp.GetComponent<Rigidbody2D>().AddForce(dir * Speed);
        Bullet tempBullet = temp.GetComponent<Bullet>();
        gm.SetBullet(tempBullet);

        audioManager.PlaySFX(SFX.Shoot);
    }
}
