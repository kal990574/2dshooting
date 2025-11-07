using UnityEngine;

public class PlayerMovementComponent : MonoBehaviour
{
    [Header("이동 범위")]
    [SerializeField] private float _xMin = -4f;
    [SerializeField] private float _xMax = 4f;
    [SerializeField] private float _yMin = -9f;
    [SerializeField] private float _yMax = 9f;

    [Header("속도 설정")]
    [SerializeField] private float _currentSpeed = 4f;
    [SerializeField] private float _speedStep = 1f;
    [SerializeField] private float _speedMul = 1.5f;

    private Vector3 _originPosition;

    void Start()
    {
        _originPosition = transform.position;
    }

    void Update()
    {
        HandleSpeedInput();
        HandleMovement();
        ApplyBoundary();
    }

    public void SpeedUp(float value)
    {
        _currentSpeed += value;
    }

    private void HandleSpeedInput()
    {
        // 속도 증가
        if (Input.GetKeyDown(KeyCode.Q))
        {
            _currentSpeed += _speedStep;
        }

        // 속도 감소
        if (Input.GetKeyDown(KeyCode.E))
        {
            _currentSpeed = Mathf.Max(0f, _currentSpeed - _speedStep);
        }
    }

    private void HandleMovement()
    {
        Vector3 moveDirection = GetMoveDirection();

        // 고속 이동
        bool isShiftPressed = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
        float moveSpeed = isShiftPressed ? _currentSpeed * _speedMul : _currentSpeed;

        transform.Translate(moveDirection * (moveSpeed * Time.deltaTime));
    }

    private Vector3 GetMoveDirection()
    {
        // 원점으로 이동
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

        // 경계
        pos.x = WrapCoordinate(pos.x, _xMin, _xMax);
        pos.y = WrapCoordinate(pos.y, _yMin, _yMax);

        transform.position = pos;
    }

    private float WrapCoordinate(float value, float min, float max)
    {
        if (value > max) return min;
        if (value < min) return max;
        return value;
    }
}