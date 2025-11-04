using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [Header("Boundary")]
    public float XMin = -2f;
    public float XMax = 2f;
    public float YMin = -5f;
    public float YMax = 5f;

    [Header("Speed")]
    public float SpeedStep = 1f;
    public float SpeedMul = 1.5f;
    public float BaseSpeed = 2f;

    public float CurrentSpeed = 0f;
    
    [Header("Start Position")]
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
        if (Input.GetKeyDown(KeyCode.Q))
        {
            BaseSpeed += SpeedStep;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            BaseSpeed = Mathf.Max(0f, BaseSpeed - SpeedStep);
        }
    }

    private void UpdateCurrentSpeed()
    {
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
        if (Input.GetKey(KeyCode.R))
        {
            return (_originPosition - transform.position).normalized;
        }

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        return new Vector2(h, v).normalized;
    }

    private void ApplyBoundary()
    {
        Vector3 pos = transform.position;

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