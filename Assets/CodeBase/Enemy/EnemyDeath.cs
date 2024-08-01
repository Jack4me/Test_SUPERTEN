using System;
using System.Collections;
using UnityEngine;

namespace CodeBase.Enemy {
    public class EnemyDeath : MonoBehaviour {
        public EnemyHealth EnemyHealth;

        // public EnemyAnimator EnemyAnimator;
        public event Action Happened;

        //  public GameObject DeathFX;
        private CoinSpawner coinSpawner;

        private void Awake() {

            coinSpawner = GetComponent<CoinSpawner>();
        }

        private void Start() {

            EnemyHealth.HealthChanged += EnemyDie;
        }

        private void OnDestroy() {
            EnemyHealth.HealthChanged -= EnemyDie;
        }

        private void EnemyDie() {
            if (EnemyHealth.CurrentHp <= 0) {
                Die();
            }
        }

        private void Die() {
            Debug.Log("ENEMY DIE");
            EnemyHealth.HealthChanged -= EnemyDie;
            coinSpawner.SpawnCoins();
            //  EnemyAnimator.PlayDeath();
            // SpawnDeathFX();
            Happened?.Invoke(); 
            StartCoroutine(DestroyGameObjWithDelay());
        }
        private IEnumerator DelayedCoinSpawn() {
            yield return null; // Задержка на один кадр
            coinSpawner.SpawnCoins();
        }
        // private void SpawnDeathFX(){
        //     Instantiate(DeathFX, transform.position, Quaternion.identity);
        // }

        private IEnumerator DestroyGameObjWithDelay(){
            yield return new WaitForSeconds(0.1f);
            Destroy(gameObject);
        }
    }
}