using UnityEngine;
using _02.Scripts.Item;
public class Enemy : MonoBehaviour
{
    [Header("데미지 설정")]
    [SerializeField] private float _damage = 1f;

    private HealthComponent _healthComponent;
    private ItemDropper _itemDropper;
    private Animator _animator;
    
    [Header("폭발 프리팹")]
    public ParticleSystem ExplosionPrefab;

    private int _score = 3000;

    void Awake()
    {
        _healthComponent = GetComponent<HealthComponent>();
        _itemDropper = GetComponent<ItemDropper>();
        _animator = GetComponent<Animator>();
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
            _animator.SetTrigger("Hit");

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
        MakeExplosionEffect();

        ScoreManager scoreManager = FindAnyObjectByType<ScoreManager>();
        scoreManager.AddScore(_score);
        Destroy(gameObject);
    }

    private void MakeExplosionEffect()
    {
        ParticleSystem explosion = Instantiate(ExplosionPrefab, transform.position, Quaternion.identity);
    }
}