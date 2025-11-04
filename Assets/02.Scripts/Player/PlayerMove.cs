using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [Header("Movement Boundary")]
    public float XMin = -2f;
    public float XMax = 2f;
    public float YMin = -5f;
    public float YMax = 5f;

    [Header("Speed Settings")]
    public float BaseSpeed = 2f;
    public float SpeedStep = 1f;
    public float SpeedMul = 1.5f;

    [Header("Runtime Info")]
    public float CurrentSpeed = 0f;

    // Private variables
    private Vector3 _originPosition;

    void Start()
    {
        CurrentSpeed = BaseSpeed;
        _originPosition = transform.position;
    }

    void Update()
    {
        HandleSpeedInput();
        UpdateCurrentSpeed();
        HandleMovement();
        ApplyBoundary();
    }

    private void HandleSpeedInput()
    {
        // Q: 속도 증가
        if (Input.GetKeyDown(KeyCode.Q))
        {
            BaseSpeed += SpeedStep;
        }

        // E: 속도 감소
        if (Input.GetKeyDown(KeyCode.E))
        {
            BaseSpeed = Mathf.Max(0f, BaseSpeed - SpeedStep);
        }
    }

    private void UpdateCurrentSpeed()
    {
        // Shift: 고속 이동
        bool isShiftPressed = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
        CurrentSpeed = isShiftPressed ? BaseSpeed * SpeedMul : BaseSpeed;
    }

    private void HandleMovement()
    {
        Vector3 moveDirection = GetMoveDirection();
        transform.Translate(moveDirection * (CurrentSpeed * Time.deltaTime));
    }

    private Vector3 GetMoveDirection()
    {
        // R: 원점으로 자동 귀환
        if (Input.GetKey(KeyCode.R))
        {
            return (_originPosition - transform.position).normalized;
        }

        // 일반 이동 
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        return new Vector2(h, v).normalized;
    }

    private void ApplyBoundary()
    {
        Vector3 pos = transform.position;

        // 화면 경계를 넘으면 반대편으로 나옴
        pos.x = WrapCoordinate(pos.x, XMin, XMax);
        pos.y = WrapCoordinate(pos.y, YMin, YMax);

        transform.position = pos;
    }

    private float WrapCoordinate(float value, float min, float max)
    {
        if (value > max) return min;
        if (value < min) return max;
        return value;
    }
}