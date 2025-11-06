using UnityEngine;

public abstract class BaseEnemy : MonoBehaviour
{
    [Header("넉백 설정")]
    [SerializeField] protected float _knockbackForce = 2f;

    [Header("체력 설정")]
    [SerializeField] protected float _maxHealth = 200f;

    [Header("데미지 설정")]
    [SerializeField] protected float _damage = 1f;

    protected float _currentHealth = 0f;

    protected virtual void Start()
    {
        _currentHealth = _maxHealth;
    }

    protected virtual void Update()
    {
        Move();
    }

    protected abstract void Move();

    protected virtual void OnTriggerEnter2D(Collider2D other)
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

    public virtual void TakeDamage(float damage)
    {
        _currentHealth -= damage;
        if (_currentHealth <= 0)
        {
            Die();
        }
    }

    public virtual void ApplyKnockback()
    {
        transform.position += Vector3.up * _knockbackForce;
    }

    protected virtual void Die()
    {
        Destroy(gameObject);
    }
}