using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyCheck : MonoBehaviour
{
    [SerializeField] private GameObject enemy1; 
    [SerializeField] private GameObject enemy2; 
    [SerializeField] private GameObject particlesPrefab;

    private bool isEnemy1Dead = false;
    private bool isEnemy2Dead = false;
    private bool hasTriggered = false; // Флаг для отслеживания вызова метода

    void Update()
    {
        CheckEnemiesStatus();
    }

    void CheckEnemiesStatus()
    {
        // Проверяем, жив ли враг 1
        if (enemy1 == null || !enemy1.activeInHierarchy)
        {
            isEnemy1Dead = true;
        }
        else
        {
            isEnemy1Dead = false;
        }

        // Проверяем, жив ли враг 2
        if (enemy2 == null || !enemy2.activeInHierarchy)
        {
            isEnemy2Dead = true;
        }
        else
        {
            isEnemy2Dead = false;
        }

        // Если оба врага мертвы и метод еще не был вызван
        if (isEnemy1Dead && isEnemy2Dead && !hasTriggered)
        {
            OnBothEnemiesDead();
            hasTriggered = true; // Устанавливаем флаг, чтобы метод больше не вызывался
        }
    }

    void OnBothEnemiesDead()
    {
        // Создаем систему частиц в позиции объекта
        if (particlesPrefab != null)
        {
            Instantiate(particlesPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("Префаб системы частиц не назначен на компоненте EnemyCheck.");
        }

       
        Debug.Log("Оба врага мертвы!");
    }
}
