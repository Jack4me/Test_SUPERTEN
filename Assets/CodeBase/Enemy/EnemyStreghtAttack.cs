using System;
using UnityEngine;

namespace CodeBase.Enemy {
    public class EnemyStreghtAttack : MonoBehaviour {
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private Transform firePoint;
        [SerializeField] private float bulletSpeed = 10f;
        [SerializeField] private float attackInterval = 2f;

        private Transform player;


        public void Shoot(Transform player) {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);


            Vector3 direction = (player.position - firePoint.position).normalized;
            bullet.GetComponent<Rigidbody>().velocity = direction * bulletSpeed;
        }

        private void OnTriggerEnter(Collider other) {
            if (other.TryGetComponent(out HeroHealth heroHealth)) {
                heroHealth.TakeDamage(1);
            }
        }
    }
}