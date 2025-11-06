using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("데미지 설정")]
    [SerializeField] private float _damage = 1f;

    private HealthComponent _healthComponent;

    void Awake()
    {
        _healthComponent = GetComponent<HealthComponent>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                player.TakeDamage(_damage);
                Die();
            }
        }
    }

    public void TakeDamage(float damage)
    {
        if (_healthComponent != null)
        {
            _healthComponent.TakeDamage(damage);
        }
    }

    public void ApplyKnockback()
    {
        if (_healthComponent != null)
        {
            _healthComponent.ApplyKnockback();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}