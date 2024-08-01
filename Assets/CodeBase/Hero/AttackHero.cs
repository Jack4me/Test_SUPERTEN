using System.Collections;
using UnityEngine;

namespace CodeBase.Hero {
    public class AttackHero : MonoBehaviour {
        [SerializeField] private GameObject arrowPrefab; 
        [SerializeField] private Transform firePoint; 
        [SerializeField] private float bulletSpeed = 10f; 
        [SerializeField] private float attackInterval = 2f; 
        [SerializeField] private int damage;
        

        private bool canShoot = true; // 

     


        public void Shoot(Transform enemy) {
            if (canShoot) {
                StartCoroutine(ShootWithInterval(enemy));
            }
        }

        private IEnumerator ShootWithInterval(Transform enemy) {
            canShoot = false;


            GameObject arrow = Instantiate(arrowPrefab, firePoint.position, firePoint.rotation);
            arrow.GetComponent<Arrow>().damage =  damage ;
            Vector3 direction = (enemy.position - firePoint.position).normalized;
            arrow.GetComponent<Rigidbody>().velocity = direction * bulletSpeed;
            Debug.Log("SHOOT");


            yield return new WaitForSeconds(attackInterval);

            canShoot = true;
        }
    }
}