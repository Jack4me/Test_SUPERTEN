
using UnityEngine;

namespace CodeBase.Enemy {
    public class EnemyBullet : MonoBehaviour {
        private void OnTriggerEnter(Collider other) {
            if (other.TryGetComponent(out HeroHealth heroHealth)) {
                Debug.Log("BAH-BAH");
                heroHealth.TakeDamage(1);
                Destroy(gameObject);
            }
            if (other.CompareTag("Obstacle")) {
                Destroy(gameObject);
            }
        }
    }
}