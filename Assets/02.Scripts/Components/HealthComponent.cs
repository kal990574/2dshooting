using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    [Header("체력 설정")]
    [SerializeField] private float _maxHealth = 300f;

    [Header("넉백 설정")]
    [SerializeField] private float _knockbackForce = 1f;

    private float _currentHealth;

    public bool IsDead => _currentHealth <= 0;
    public float HealthPercentage => _currentHealth / _maxHealth;

    void Start()
    {
        _currentHealth = _maxHealth;
    }

    public void TakeDamage(float damage)
    {
        _currentHealth -= damage;
    }

    public void ApplyKnockback()
    {
        transform.position += Vector3.up * _knockbackForce;
    }

    public void Heal(float amount)
    {
        _currentHealth += amount;
        _currentHealth = Mathf.Min(_currentHealth, _maxHealth);
    }

    public void ResetHealth()
    {
        _currentHealth = _maxHealth;
    }
}