using UnityEngine;

public class PetBulletMovement : MonoBehaviour
{
    [Header("속도 설정")]
    [SerializeField] private float _speed = 5f;

    [Header("데미지 설정")]
    [SerializeField] private float _damage = 10f;

    void Update()
    {
        Move();
    }

    private void Move()
    {
        transform.position += Vector3.up * (_speed * Time.deltaTime);
    }

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
        gameObject.SetActive(false);
    }
}
