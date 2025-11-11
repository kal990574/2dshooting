using UnityEngine;

public class BoomCollider : MonoBehaviour
{
    private const float Damage = 9999f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        ApplyDamageToEnemy(other);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        ApplyDamageToEnemy(other);
    }

    private void ApplyDamageToEnemy(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (other.TryGetComponent<Enemy>(out var enemy))
            {
                enemy.TakeDamage(Damage);
            }
        }
    }
}