using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelComplete : MonoBehaviour
{
   [SerializeField] private TextMeshProUGUI levelCompleteText; 

   private void OnTriggerEnter(Collider other) {
      if (other.GetComponent<HeroHealth>()) {
         levelCompleteText.text = "Level Complete!";
         levelCompleteText.fontSize = 60;
         levelCompleteText.gameObject.SetActive(true);
      }
   }
}
