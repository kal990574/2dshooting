using UnityEngine;

public class Player : MonoBehaviour
{
    private HealthComponent _healthComponent;
    private PlayerMovementComponent _movementComponent;
    private PlayerFireComponent _fireComponent;

    void Awake()
    {
        _healthComponent = GetComponent<HealthComponent>();
        _movementComponent = GetComponent<PlayerMovementComponent>();
        _fireComponent = GetComponent<PlayerFireComponent>();
    }

    public void TakeDamage(float damage)
    {
        if (_healthComponent != null)
        {
            _healthComponent.TakeDamage(damage);
            if (_healthComponent.IsDead)
            {
                Die();
            }
        }
    }

    public void IncreaseSpeed(float amount)
    {
        if (_movementComponent != null)
        {
            _movementComponent.SpeedUp(amount);
        }
    }

    public void IncreaseHealth(float amount)
    {
        if (_healthComponent != null)
        {
            _healthComponent.Heal(amount);
        }
    }

    public void IncreaseAttackSpeed(float amount)
    {
        if (_fireComponent != null)
        {
            _fireComponent.IncreaseAttackSpeed(amount);
        }
    }
    private void Die()
    {
        Destroy(gameObject);
    }
}