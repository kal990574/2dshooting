using UnityEngine;

public class BossBulletMovement : MonoBehaviour
{
    [Header("속도 설정")]
    [SerializeField] private float _speed = 5f;

    private Vector3 _direction = Vector3.down;

    public void SetDirection(Vector3 direction)
    {
        _direction = direction.normalized;
    }

    void Update()
    {
        Move();
    }

    private void Move()
    {
        transform.position += _direction * (_speed * Time.deltaTime);
    }
}