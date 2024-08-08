using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Bullet : MonoBehaviour
{
    const float MaxDistance = 130f;
    const float CameraBeforeMax = MaxDistance - 12f;
    public UnityAction<bool> funcDestroy;
    public Rigidbody2D rb2D;

    public bool isHit = false;
    public float distance = 0f;
    public Camera mainCamera;

    float bulletSpeed = -1f;
    private void OnDestroy()
    {
        funcDestroy?.Invoke(isHit);
    }

    private void Update()
    {
        if (bulletSpeed <= 0f)
        {
            bulletSpeed = rb2D.velocity.magnitude;
            //Debug.Log($"Speed = {bulletSpeed}");
        }

        if(bulletSpeed > 0f)
            distance += bulletSpeed * Time.deltaTime;
        if (distance >= MaxDistance)
            Destroy(gameObject);

        if (distance >= CameraBeforeMax)
            mainCamera = null;

        if (mainCamera != null && distance >= 9f)
        {
            Vector3 current = transform.position;
            current.z = -10f;
            mainCamera.transform.localPosition = current;
        }
    }
}
