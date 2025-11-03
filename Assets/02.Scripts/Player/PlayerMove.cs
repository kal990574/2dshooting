using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [Header("Boundary")]
    [SerializeField] private float _xMin = -2f;
    [SerializeField] private float _xMax = 2f;
    [SerializeField] private float _yMin = -3f;
    [SerializeField] private float _yMax = 3f;

    [Header("Speed")]
    [SerializeField] private float _speedStep = 1f;
    [SerializeField] private float _speedMul = 3f;
    [SerializeField] private float _baseSpeed = 1f;

    private float _currentSpeed;

    void Start()
    {
        _currentSpeed = _baseSpeed;
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
            _baseSpeed += _speedStep;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            _baseSpeed = Mathf.Max(0f, _baseSpeed - _speedStep);
        }
    }

    private void UpdateCurrentSpeed()
    {
        bool isShiftPressed = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
        _currentSpeed = isShiftPressed ? _baseSpeed * _speedMul : _baseSpeed;
    }

    private void HandleMovement()
    {
        Vector3 moveDirection = GetMoveDirection();
        transform.Translate(moveDirection * _currentSpeed * Time.deltaTime);
    }

    private Vector3 GetMoveDirection()
    {
        if (Input.GetKey(KeyCode.R))
        {
            return (Vector3.zero - transform.position).normalized;
        }

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        return new Vector2(h, v).normalized;
    }

    private void ApplyBoundary()
    {
        Vector3 pos = transform.position;

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