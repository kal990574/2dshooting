using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("플레이어 체력")]
    public int MaxHealth = 3;
    public int CurrentHealth;

    void Start()
    {
        CurrentHealth = MaxHealth;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            TakeDamage(1);
            Destroy(other.gameObject);
        }
    }

    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;

        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}