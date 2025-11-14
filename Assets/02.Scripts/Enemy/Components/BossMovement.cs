using UnityEngine;

public class BossMovement : MovementComponent
{
    [Header("정지 위치")]
    [SerializeField] private float _stopYPosition = 4f;

    protected override void Move()
    {
        if (transform.position.y > _stopYPosition)
        {
            transform.Translate(Vector3.down * (_speed * Time.deltaTime));
        }
        else
        {
            if (transform.position.y < _stopYPosition)
            {
                transform.position = new Vector3(
                    transform.position.x,
                    _stopYPosition,
                    transform.position.z
                );
            }
        }
    }
}