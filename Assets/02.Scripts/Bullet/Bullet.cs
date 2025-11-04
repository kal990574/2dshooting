using UnityEngine;
using UnityEngine.Serialization;

public class Bullet : MonoBehaviour
{
    [Header("Speed Settings")]
    public float StartSpeed = 1f;
    public float EndSpeed = 7f;
    public float AccelerationTime = 1.2f;
    public float CurrentSpeed;

    [Header("Move Settings")]
    public int MoveMode = 0;
    // 좌우 이동 속도
    public float HorizontalSpeed = 3f;
    // 좌우 이동 범위
    public float HorizontalRange = 1f;
    // 회전 속도 
    public float RotationSpeed = 720f;
    // 시작 x 좌표
    private float _startX;
    // 수평 방향
    private float _horizontalDirection = 1f; 
    
    void Start()
    {
        // 처음 speed 저장
        CurrentSpeed = StartSpeed;
        // 처음 생성된 x 좌표 저장
        _startX = transform.position.x; 
    }

    void Update()
    {
        // 가속
        float acceleration = (EndSpeed - CurrentSpeed) / AccelerationTime;
        CurrentSpeed += acceleration * Time.deltaTime;
        CurrentSpeed = Mathf.Min(CurrentSpeed, EndSpeed);

        // 이동 모드에 따른 움직임
        if (MoveMode == 1)
        {
            // 수직
            transform.position += Vector3.up * (Time.deltaTime * CurrentSpeed);

            // 수평
            float horizontalMovement = _horizontalDirection * HorizontalSpeed * Time.deltaTime;
            transform.position = new Vector3(transform.position.x + horizontalMovement, transform.position.y, transform.position.z);

            // 오른쪽으로 방향 전환
            if (transform.position.x <= _startX - HorizontalRange)
            {
                transform.position = new Vector3(_startX - HorizontalRange, transform.position.y, transform.position.z);
                _horizontalDirection = 1f;
            }
            // 왼쪽으로 방향 전환
            else if (transform.position.x >= _startX + HorizontalRange)
            {
                transform.position = new Vector3(_startX + HorizontalRange, transform.position.y, transform.position.z);
                _horizontalDirection = -1f;
            }
        }
        
        else if (MoveMode == 2)
        {
            // 회전 관련 
            transform.Translate(Vector3.up * (Time.deltaTime * CurrentSpeed));
            transform.Rotate(0f, 0f, RotationSpeed * Time.deltaTime);
            
            // 수직 이동
            transform.position += Vector3.up * (Time.deltaTime * CurrentSpeed);
        }
        else
        {
            // 수직 이동
            transform.position += Vector3.up * (Time.deltaTime * CurrentSpeed);
        }
    }
}
