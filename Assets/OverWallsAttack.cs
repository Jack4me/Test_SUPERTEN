using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverWallsAttack : MonoBehaviour
{
     [SerializeField] private GameObject bulletPrefab; 
    [SerializeField] private Transform firePoint; 
    [SerializeField] private float bulletSpeed = 10f; 
    [SerializeField] private float attackInterval = 2f; 
    [SerializeField] private float gravity = -9.81f; 

    private Transform player; 
    private void Start()
    {
        StartCoroutine(AttackRoutine());
    }

    private IEnumerator AttackRoutine()
    {
        while (true)
        {
            if (player != null)
            {
                Shoot(player);
            }
            yield return new WaitForSeconds(attackInterval);
        }
    }

    public void Shoot(Transform target)
    {
        if (bulletPrefab == null || firePoint == null)
        {
            Debug.LogError("Bullet prefab or fire point is not assigned.");
            return;
        }

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Bullet prefab must have a Rigidbody component.");
            return;
        }

        Vector3 direction = CalculateTrajectory(firePoint.position, target.position, bulletSpeed, gravity);
        if (direction == Vector3.zero)
        {
            Debug.LogError("Calculated direction is zero. Unable to shoot.");
            return;
        }

        rb.velocity = direction;
    }

    private Vector3 CalculateTrajectory(Vector3 startPosition, Vector3 targetPosition, float speed, float gravity)
    {
        Vector3 toTarget = targetPosition - startPosition;
        float distance = toTarget.magnitude;
        float height = toTarget.y;
        float horizontalDistance = Mathf.Sqrt(toTarget.x * toTarget.x + toTarget.z * toTarget.z);

        float angle = Mathf.Atan2(height, horizontalDistance);
        
        float velocityY = speed * Mathf.Sin(angle);
        float velocityXZ = speed * Mathf.Cos(angle);

        Vector3 direction = toTarget.normalized;
        direction.y = 0;
        direction *= velocityXZ;
        direction.y = velocityY;

        float timeToReach = distance / (speed * Mathf.Cos(angle));
        direction.y -= 0.5f * gravity * timeToReach;

        return direction;
    }
}
