using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public enum MovementMode
    {
        Straight = 0,      // 직선
        SideToSide = 1,    // 좌우 이동
        Rotating = 2       // 나선형
    }

    [Header("속도 설정")]
    public float StartSpeed = 1f;
    public float EndSpeed = 7f;
    public float AccelerationTime = 1.2f;
    public float CurrentSpeed;

    [Header("이동 설정")]
    public MovementMode MoveMode = MovementMode.Straight;

    [Header("좌우 이동 설정")]
    public float HorizontalSpeed = 3f;
    public float HorizontalRange = 1f;

    [Header("회전 설정")]
    public float RotationSpeed = 480f;

    [Header("데미지 설정")]
    public float Damage = 10f;

    private float _startX;
    private float _horizontalDirection = 1f;

    void Start()
    {
        CurrentSpeed = StartSpeed;
        _startX = transform.position.x;
    }

    void Update()
    {
        UpdateSpeed();
        UpdateMovement();
    }

    private void UpdateSpeed()
    {
        if (CurrentSpeed < EndSpeed)
        {
            float acceleration = (EndSpeed - StartSpeed) / AccelerationTime;
            CurrentSpeed += acceleration * Time.deltaTime;
            CurrentSpeed = Mathf.Min(CurrentSpeed, EndSpeed);
        }
    }

    private void UpdateMovement()
    {
        switch (MoveMode)
        {
            case MovementMode.Straight:
                MoveStraight();
                break;
            case MovementMode.SideToSide:
                MoveSideToSide();
                break;
            case MovementMode.Rotating:
                MoveRotating();
                break;
        }
    }

    private void MoveStraight()
    {
        transform.position += Vector3.up * (CurrentSpeed * Time.deltaTime);
    }

    private void MoveSideToSide()
    {
        // 수직 이동
        transform.position += Vector3.up * (CurrentSpeed * Time.deltaTime);

        // 수평 이동
        float horizontalMovement = _horizontalDirection * HorizontalSpeed * Time.deltaTime;
        Vector3 newPos = transform.position;
        newPos.x += horizontalMovement;

        // 방향 전환
        if (newPos.x <= _startX - HorizontalRange)
        {
            newPos.x = _startX - HorizontalRange;
            _horizontalDirection = 1f;
        }
        else if (newPos.x >= _startX + HorizontalRange)
        {
            newPos.x = _startX + HorizontalRange;
            _horizontalDirection = -1f;
        }

        transform.position = newPos;
    }

    private void MoveRotating()
    {
        // 나선형 회전 방식
        transform.Translate(Vector3.up * (CurrentSpeed * Time.deltaTime));
        transform.Rotate(0f, 0f, RotationSpeed * Time.deltaTime);
        
        transform.position += Vector3.up * (CurrentSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }
}