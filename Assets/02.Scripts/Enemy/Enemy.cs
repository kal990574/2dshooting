using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("이동 설정")]
    public float Speed = 3f;

    [Header("넉백 설정")]
    public float KnockbackForce = 2f;

    private float _maxHealth = 200f;
    private float _currentHealth = 0f;
    private float _damage = 1f;

    void Start()
    {
        _currentHealth = _maxHealth;
    }

    void Update()
    {
        transform.Translate(Vector3.down * (Speed * Time.deltaTime));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            player.TakeDamage(_damage);

            Die();
        }
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
        transform.position += Vector3.up * KnockbackForce;
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
