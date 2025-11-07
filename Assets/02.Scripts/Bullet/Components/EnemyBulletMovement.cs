using UnityEngine;

public class EnemyBulletMovement : MonoBehaviour
{
    [Header("속도 설정")]
    [SerializeField] private float _speed = 5f;

    void Update()
    {
        Move();
    }

    private void Move()
    {
        transform.position += Vector3.down * (_speed * Time.deltaTime);
    }
}