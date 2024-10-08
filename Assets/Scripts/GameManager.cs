using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public enum GameState
{
    BeforeStart = 0,
    Aiming = 1,
    Shoot = 2,
    EndShoot = 3,
    Inventory = 4,
    GameEnd = 5,
}

public class GameManager : Singleton
{
    public GameState gameState = GameState.BeforeStart;

    public GameObject objInventory;
    public Bullet currnetBullet;

    public Image imgCurrentBright;
    public Camera mainCamera;

    static Vector3 CameraOriginPos = new Vector3(0, 0, -10);
    private void Awake()
    {
        gm = this;
    }

    private void Start()
    {
        StartCoroutine(InGame());
    }

    IEnumerator InGame()
    {
        yield return null;

        gameState = GameState.BeforeStart;
        ResetGameUI();

        objectSpawner.SpawnInitial();
        gameState = GameState.BeforeStart;

        yield return new WaitUntil(() => gameState != GameState.BeforeStart);

        objectSpawner.SpawnRandom();
    }

    public void SetBullet(Bullet bullet)
    {
        currnetBullet = bullet;
        currnetBullet.funcDestroy += BulletDestroyed;
        currnetBullet.mainCamera = mainCamera;
    }

    public void BulletDestroyed(bool isHit)
    {
        if (isHit)
            StartCoroutine(Shot());
        else
        {
            if(mainCamera != null)
                mainCamera.transform.position = CameraOriginPos;
        }
    }

    IEnumerator Shot()
    {
        yield return new WaitForSeconds(2f);
        mainCamera.transform.position = CameraOriginPos;
        float value = (objectSpawner.destroyedTargetCount / (float)ObjectSpawner.MaxTargetCount);
        yield return ChangeColor(mainCamera.backgroundColor, new Color(value, value, value), 2f);
        gameState = GameState.Aiming;
    }

    IEnumerator ChangeColor(Color colorOrigin,  Color colorDestination, float duration)
    {
        mainCamera.backgroundColor = colorOrigin;
        float time = 0f;
        while(time < duration)
        {
            mainCamera.backgroundColor = Color.Lerp(colorOrigin, colorDestination, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
    }

    public void ResetGameUI()
    {
        shooter.gameObject.SetActive(true);

    }

    public void GoToAiming()
    {
        gameState = GameState.Aiming;
    }

    public void GoToShoot()
    {
        gameState = GameState.Shoot;
    }

    public void GoToEndShoot()
    {
        gameState = GameState.EndShoot;
    }

    public void GoToInventory()
    {
        gameState = GameState.Inventory;
    }

    public void GoToGameEnd()
    {
        gameState = GameState.GameEnd;

    }
}
