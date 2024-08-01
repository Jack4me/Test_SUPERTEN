using System;

public interface IHealth {
    float CurrentHp{ get; set; }
    float MaxHp{ get; set; }
    event Action HealthChanged;
    void TakeDamage(float damage);
}