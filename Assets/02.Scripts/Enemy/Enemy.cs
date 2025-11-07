using UnityEngine;
using _02.Scripts.Item;
public class Enemy : MonoBehaviour
{
    [Header("데미지 설정")]
    [SerializeField] private float _damage = 1f;

    private HealthComponent _healthComponent;
    private ItemDropper _itemDropper;

    void Awake()
    {
        _healthComponent = GetComponent<HealthComponent>();
        _itemDropper = GetComponent<ItemDropper>();
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

            if (_healthComponent.IsDead)
            {
                Die();
            }
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
        if (_itemDropper != null)
        {
            _itemDropper.TryDropItem();
        }

        Destroy(gameObject);
    }
}