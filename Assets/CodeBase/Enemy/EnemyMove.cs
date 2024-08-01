using System;
using System.Collections;
using CodeBase.Enemy;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class EnemyMove : MonoBehaviour {
    public Transform player; 
    public float speed = 3.5f; 
    public float detectionRadius = 10f; 
    public float moveInterval = 3f; 

    private NavMeshAgent agent;
    private EnemyStreghtAttack enemyStreghtAttack;

    private void Awake() {
        agent = GetComponent<NavMeshAgent>();
        enemyStreghtAttack = GetComponent<EnemyStreghtAttack>();
    }

    void Start() {
      
        agent.speed = speed;
        StartCoroutine(MoveRandomly());
    }


    IEnumerator MoveRandomly() {
        while (true) {
            if (player == null) {
                yield break;
            }
            Vector3 randomDirection = Random.insideUnitSphere * detectionRadius;
            randomDirection += player.position;
            NavMeshHit navHit;
            NavMesh.SamplePosition(randomDirection, out navHit, detectionRadius, -1);

            agent.SetDestination(navHit.position);

            yield return new WaitForSeconds(moveInterval);

            TurnToPlayer();
            enemyStreghtAttack.Shoot(player.transform);
            yield return new WaitForSeconds(moveInterval);
        }
    }

    private void TurnToPlayer() {
        if (player == null) {
           return;
        }
        Vector3 directionToPlayer = player.position - transform.position;
        directionToPlayer.y = 0; 
        if (directionToPlayer != Vector3.zero)
        {
            Quaternion rotation = Quaternion.LookRotation(directionToPlayer);
            transform.rotation = rotation;
        }
    }

    void OnDrawGizmosSelected() {
        // Визуализация радиуса перемещения
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(player.position, detectionRadius);
    }
}