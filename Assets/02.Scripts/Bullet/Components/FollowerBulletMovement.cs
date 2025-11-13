using UnityEngine;

public class FollowerBulletMovement : MonoBehaviour
{
    [Header("속도 설정")]
    [SerializeField] private float _speed = 5f;

    void Update()
    {
        Move();
    }

    private void Move()
    {
        transform.position += Vector3.up * (_speed * Time.deltaTime);
    }
}
