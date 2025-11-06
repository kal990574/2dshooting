using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [Header("데미지 설정")]
    [SerializeField] private float _damage = 1f;

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

    private void Die()
    {
        Destroy(gameObject);
    }
}