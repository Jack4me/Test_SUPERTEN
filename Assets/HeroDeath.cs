using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroDeath : MonoBehaviour {
 [SerializeField]   private HeroHealth heroHealth;
 

  // public EnemyAnimator EnemyAnimator;

  //  public GameObject DeathFX;
  private CoinSpawner coinSpawner;

  private void Awake() {

   coinSpawner = GetComponent<CoinSpawner>();
  }

  private void Start() {

   heroHealth.Die += HeroDie;
  }

  private void OnDestroy() {
   heroHealth.Die -= HeroDie;
  }

  private void HeroDie() {
   
   StartCoroutine(DestroyGameObjWithDelay());

  }


  private IEnumerator DestroyGameObjWithDelay(){
   yield return new WaitForSeconds(0.1f);
   Destroy(gameObject);
  }
 }