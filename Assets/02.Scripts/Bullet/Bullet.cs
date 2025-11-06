using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("데미지 설정")]
    [SerializeField] private float _damage = 10f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Hitbox"))
        {
            Hitbox hitbox = other.GetComponent<Hitbox>();
            if (hitbox != null)
            {
                hitbox.TakeDamage(_damage);
                Die();
            }
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}