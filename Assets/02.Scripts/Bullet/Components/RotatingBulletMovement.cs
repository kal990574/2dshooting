using UnityEngine;

public class RotatingBulletMovement : BulletMovementComponent
{
    [Header("회전 설정")]
    [SerializeField] private float _rotationSpeed = 480f;

    protected override void Move()
    {
        transform.Translate(Vector3.up * (GetCurrentSpeed() * Time.deltaTime));
        transform.Rotate(0f, 0f, _rotationSpeed * Time.deltaTime);

        transform.position += Vector3.up * (GetCurrentSpeed() * Time.deltaTime);
    }
}