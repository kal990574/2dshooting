using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public enum MovementMode
    {
        Straight = 0,      
        SideToSide = 1,    
        Rotating = 2       
    }

    [Header("Speed Settings")]
    public float StartSpeed = 1f;
    public float EndSpeed = 7f;
    public float AccelerationTime = 1.2f;
    public float CurrentSpeed;

    [Header("Move Settings")]
    public MovementMode MoveMode = MovementMode.Straight;

    [Header("Side to Side Settings")]
    public float HorizontalSpeed = 3f;
    public float HorizontalRange = 1f;

    [Header("Rotating Settings")]
    public float RotationSpeed = 480f;

    // Private variables
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

        // 경계 체크 및 방향 전환
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
        if (other.CompareTag("Enemy") == false) return;
        
            Destroy(gameObject);
            Enemy enemy = other.GetComponent<Enemy>();
            Destroy(other.gameObject);
    }
}