using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObjectSpawner : Singleton
{
    public const int MaxTargetCount = 10;
    public const int MaxObstacleCount = 3;
    static Vector2 InitialTargetPos = new Vector2(0, 0);

    public GameObject objTarget;
    public GameObject objObstacle;
    public int destroyedTargetCount = 0;
    [NonSerialized] public List<Target> targets = new List<Target>();
    [NonSerialized] public List<Obstacle> obstacles = new List<Obstacle>();

    private void Awake()
    {
        objectSpawner = this;
    }

    public void SpawnInitial()
    {
        GameObject temp = Instantiate(objTarget, objTarget.transform.parent);
        temp.SetActive(true);
        temp.transform.position = InitialTargetPos;
        targets.Add(temp.GetComponent<Target>());
    }

    public void SpawnRandom()
    {
        List<int> distances = new List<int>();

        //1. Spawn Target
        for (int i = 1; i < MaxTargetCount; i++)
        {
            int tempDist = Random.Range((i + 1) * 12 - 3, (i + 1) * 12 + 3);
            float angle = Random.Range(-45f, 45f);
            GameObject temp = Instantiate(objTarget, new Vector3(tempDist * Mathf.Cos(angle * Mathf.Deg2Rad), tempDist * Mathf.Sin(angle * Mathf.Deg2Rad)), Quaternion.Euler(0, 0, 0), objTarget.transform.parent);
            temp.SetActive(true);
            targets.Add(temp.GetComponent<Target>());
            Debug.Log($"{i}th Object Distance = {temp.transform.position.magnitude}");
        }

        //2. Spawn Obstacle
        int obstacleCount = Random.Range(1, MaxObstacleCount + 1);
    }

    public void DestroyTarget(Target current)
    {
        destroyedTargetCount++;
        targets.Remove(current);
    }
}
