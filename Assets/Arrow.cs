using System;
using System.Collections;
using System.Collections.Generic;
using CodeBase.Hero;
using UnityEngine;

public class Arrow : MonoBehaviour {
    public int damage;

    private void OnTriggerEnter(Collider other) {
        if (other.TryGetComponent(out EnemyHealth enemyHealth)) {
            enemyHealth.TakeDamage(1);
            Destroy(gameObject);
        }

        if (other.CompareTag("Obstacle")) {
            Destroy(gameObject);
        }
    }
}