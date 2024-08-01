using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class GameStarter : MonoBehaviour {

    public TextMeshProUGUI countdownText; // TextMeshPro для отображения отсчета

    void Start()
    {
        Time.timeScale = 0f; // Останавливаем игровое время
        StartCoroutine(StartGameAfterCountdown(3f)); // Запускаем корутину для отсчета
    }

    IEnumerator StartGameAfterCountdown(float countdownTime)
    {
        float remainingTime = countdownTime;

        while (remainingTime > 0)
        {
            countdownText.text = remainingTime.ToString("F0"); // Обновляем текст с оставшимся временем
            yield return new WaitForSecondsRealtime(1f); // Ждем 1 реальную секунду
            remainingTime--;
        }

      
        yield return new WaitForSecondsRealtime(1f); // Ждем 1 реальную секунду, чтобы текст "Go!" оставался видимым

        countdownText.gameObject.SetActive(false); // Отключаем текст отсчета
        Time.timeScale = 1f; // Восстанавливаем игровое время
    }

   
}