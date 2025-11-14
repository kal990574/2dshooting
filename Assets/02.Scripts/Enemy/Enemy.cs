using UnityEngine;
using _02.Scripts.Item;
public class Enemy : MonoBehaviour
{
    [Header("데미지 설정")]
    [SerializeField] private float _damage = 1f;

    protected HealthComponent _healthComponent;
    private ItemDropper _itemDropper;
    protected Animator _animator;
    
    [Header("폭발 프리팹")]
    public ParticleSystem ExplosionPrefab;

    protected int _score = 100000;

    protected virtual void Awake()
    {
        _healthComponent = GetComponent<HealthComponent>();
        _itemDropper = GetComponent<ItemDropper>();
        _animator = GetComponent<Animator>();
    }

    protected virtual void OnEnable()
    {
        // Health 초기화
        if (_healthComponent != null)
        {
            _healthComponent.ResetHealth();
        }

        // Chasing Enemy의 경우 Player 재탐색
        ChasingMovement chasingMovement = GetComponent<ChasingMovement>();
        if (chasingMovement != null)
        {
            chasingMovement.FindPlayer();
        }
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

    protected virtual void Die()
    {
        if (_itemDropper != null)
        {
            _itemDropper.TryDropItem();
        }
        MakeExplosionEffect();

        ScoreManager scoreManager = FindAnyObjectByType<ScoreManager>();
        scoreManager.AddScore(_score);
        gameObject.SetActive(false);
    }

    protected void MakeExplosionEffect()
    {
        ParticleSystem explosion = Instantiate(ExplosionPrefab, transform.position, Quaternion.identity);
    }
}