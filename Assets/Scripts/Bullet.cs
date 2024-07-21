using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Bullet : MonoBehaviour
{
    const float MaxDistance = 100f;
    public UnityAction funcDestroy;
    public Rigidbody2D rb2D;

    public float distance = 0f;

    private void OnDestroy()
    {
        funcDestroy?.Invoke();
    }

    private void Update()
    {
        distance += rb2D.velocity.SqrMagnitude() * Time.deltaTime;
        if (distance >= MaxDistance)
            Destroy(gameObject);
    }
}
