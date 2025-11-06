using UnityEngine;

public class PlayerMovementComponent : MonoBehaviour
{
    [Header("이동 범위")]
    [SerializeField] private float _xMin = -4f;
    [SerializeField] private float _xMax = 4f;
    [SerializeField] private float _yMin = -9f;
    [SerializeField] private float _yMax = 9f;

    [Header("속도 설정")]
    [SerializeField] private float _baseSpeed = 4f;
    [SerializeField] private float _speedStep = 1f;
    [SerializeField] private float _speedMul = 1.5f;

    [Header("실행 정보")]
    [SerializeField] private float _currentSpeed = 0f;

    private Vector3 _originPosition;

    void Start()
    {
        _currentSpeed = _baseSpeed;
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
        // 속도 증가
        if (Input.GetKeyDown(KeyCode.Q))
        {
            _baseSpeed += _speedStep;
        }

        // 속도 감소
        if (Input.GetKeyDown(KeyCode.E))
        {
            _baseSpeed = Mathf.Max(0f, _baseSpeed - _speedStep);
        }
    }

    private void UpdateCurrentSpeed()
    {
        // Shift: 고속 이동
        bool isShiftPressed = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
        _currentSpeed = isShiftPressed ? _baseSpeed * _speedMul : _baseSpeed;
    }

    private void HandleMovement()
    {
        Vector3 moveDirection = GetMoveDirection();
        transform.Translate(moveDirection * (_currentSpeed * Time.deltaTime));
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

        // 화면 경계를 넘으면 반대편으로 나옴
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