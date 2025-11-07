using UnityEngine;

public class SideToSideMovement : BulletMovementComponent
{
    [Header("좌우 이동 설정")]
    [SerializeField] private float _horizontalSpeed = 3f;
    [SerializeField] private float _horizontalRange = 1f;

    private float _startX;
    private float _horizontalDirection = 1f;

    protected override void Start()
    {
        base.Start();
        _startX = transform.position.x;
    }

    protected override void Move()
    {
        // 수직 이동
        transform.position += Vector3.up * (GetCurrentSpeed() * Time.deltaTime);

        // 수평 이동
        float horizontalMovement = _horizontalDirection * _horizontalSpeed * Time.deltaTime;
        Vector3 newPos = transform.position;
        newPos.x += horizontalMovement;

        // 방향 전환
        if (newPos.x <= _startX - _horizontalRange)
        {
            newPos.x = _startX - _horizontalRange;
            _horizontalDirection = 1f;
        }
        else if (newPos.x >= _startX + _horizontalRange)
        {
            newPos.x = _startX + _horizontalRange;
            _horizontalDirection = -1f;
        }

        transform.position = newPos;
    }
}