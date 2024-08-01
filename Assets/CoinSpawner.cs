using UnityEngine;

public class CoinSpawner : MonoBehaviour {
    public GameObject coinPrefab;
    public int numberOfCoins = 10;
    [SerializeField] private GameObject coins;

    public  void SpawnCoins() {
        for (int i = 0; i < numberOfCoins; i++) {
            float angle = Random.Range(0f, 2f * Mathf.PI);

            Vector3 coinPosition = new Vector3(
                Mathf.Cos(angle) * 2,
                0,
                Mathf.Sin(angle) * 2
            );


            Instantiate(coinPrefab, coinPosition + transform.position + Vector3.up,
                Quaternion.Euler(new Vector3(45, 90, 90)), coins.transform);
        }
    }
}