
using System;
using System.Collections;
using UnityEngine;

public class AroundAttack : MonoBehaviour
{
     public GameObject bulletPrefab; // Префаб пули
    public Transform shootPoint; // Точка, откуда будут вылетать пули
    public float shootInterval = 0.5f; // Интервал между выстрелами
    public float rotationDuration = 2f; // Продолжительность одного полного вращения в секундах

    private bool isShooting = false; // Флаг для контроля стрельбы
    private float timeSinceLastShot = 0f; // Время, прошедшее с последнего выстрела

    void Start()
    {
        StartCoroutine(RotateAndShoot());
    }

    IEnumerator RotateAndShoot()
    {
        while (true)
        {
            isShooting = true; // Начинаем стрельбу
            timeSinceLastShot = 0f; // Сброс времени с последнего выстрела
            float elapsedTime = 0f;
            float startRotation = transform.eulerAngles.y;
            float endRotation = startRotation + 360f;

            while (elapsedTime < rotationDuration)
            {
                transform.eulerAngles = new Vector3(
                    transform.eulerAngles.x,
                    Mathf.Lerp(startRotation, endRotation, elapsedTime / rotationDuration),
                    transform.eulerAngles.z
                );

                // Обновляем время с последнего выстрела и стреляем, если интервал истек
                timeSinceLastShot += Time.deltaTime;
                if (timeSinceLastShot >= shootInterval)
                {
                    ShootBullet(); // Выполняем выстрел
                    timeSinceLastShot = 0f; // Сброс времени с последнего выстрела
                }

                elapsedTime += Time.deltaTime;
                yield return null;
            }

            // Убедитесь, что вращение завершено точно на 360 градусов
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, endRotation, transform.eulerAngles.z);
            
            isShooting = false; // Завершаем стрельбу
            yield return new WaitForSeconds(rotationDuration); // Ждем перед следующим вращением
        }
    }

    void ShootBullet()
    {
        if (bulletPrefab != null && shootPoint != null)
        {
            // Создаем пулю
            GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);

            // Получаем компонент Rigidbody и устанавливаем скорость
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = shootPoint.forward * 10f; // Задаем скорость пули
            }

            // Опционально, можно настроить время жизни пули
            Destroy(bullet, 5f); // Удаляем пулю через 5 секунд
        }
    }
}


