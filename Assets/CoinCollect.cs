using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollect : MonoBehaviour
{
    private float coinMoveSpeed = 10;

    private void OnTriggerEnter(Collider other) {
        Debug.Log(other.name + "COllect");
        if (other.TryGetComponent(out Coin coin)) {
            Debug.Log( "Statr Collect COINS");
            StartCoroutine(MoveCoinToPlayer(coin));
        }
    }
    private IEnumerator MoveCoinToPlayer(Coin coin) {
        Vector3 startPosition = coin.transform.position;
        Vector3 targetPosition = transform.position; // Позиция игрока
        float journeyLength = Vector3.Distance(startPosition, targetPosition);
        float startTime = Time.time;

        while (coin != null && Vector3.Distance(coin.transform.position, targetPosition) > 0.1f) {
            float distanceCovered = (Time.time - startTime) * coinMoveSpeed;
            float fractionOfJourney = distanceCovered / journeyLength;
            coin.transform.position = Vector3.Lerp(startPosition, targetPosition, fractionOfJourney);

            yield return null;
        }

        // Удаляем монетку после достижения игрока
        Destroy(coin.gameObject);
    }
}