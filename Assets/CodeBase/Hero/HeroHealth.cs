using System;
using UnityEngine;

// [RequireComponent(typeof(HeroAnimator))]
public class HeroHealth : MonoBehaviour, IHealth{
    //public HeroAnimator heroAnimator;
    public event Action HealthChanged;
    public event Action Die;

    [SerializeField] private float _currentHp;
    [SerializeField] private float _maxHp;

      
    public float CurrentHp{
        get{ return _currentHp; }
        set{ _currentHp = value; }
    }

    public float MaxHp{
        get{ return _maxHp; }
        set{ _maxHp = value; }
    }


   
        

    public void TakeDamage(float damage){
            
        if (CurrentHp <= 0){
            Die?.Invoke();        }
        CurrentHp -= damage;
        HealthChanged?.Invoke();
        // heroAnimator.PlayHit();
    }
}