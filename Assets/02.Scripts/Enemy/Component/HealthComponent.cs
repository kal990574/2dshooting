using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    [Header("체력 설정")]
    [SerializeField] private float _maxHealth = 300f;

    [Header("넉백 설정")]
    [SerializeField] private float _knockbackForce = 1f;

    private float _currentHealth;

    void Start()
    {
        _currentHealth = _maxHealth;
    }

    public void TakeDamage(float damage)
    {
        _currentHealth -= damage;

        if (_currentHealth <= 0)
        {
            Die();
        }
    }

    public void ApplyKnockback()
    {
        transform.position += Vector3.up * _knockbackForce;
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}