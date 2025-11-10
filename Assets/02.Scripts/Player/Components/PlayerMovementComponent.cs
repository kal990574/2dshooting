using UnityEngine;

[RequireComponent(typeof(PlayerInputHandler))]
[RequireComponent(typeof(AutoMovement))]
public class PlayerMovementComponent : MonoBehaviour
{
    [Header("이동 범위")]
    [SerializeField] private float _xMin = -4f;
    [SerializeField] private float _xMax = 4f;
    [SerializeField] private float _yMin = -9f;
    [SerializeField] private float _yMax = 9f;

    [Header("속도 설정")]
    [SerializeField] private float _currentSpeed = 4f;
    [SerializeField] private float _speedMul = 1.5f;

    private PlayerInputHandler _inputHandler;
    private AutoMovement _autoMovement;

    void Awake()
    {
        _inputHandler = GetComponent<PlayerInputHandler>();
        _autoMovement = GetComponent<AutoMovement>();
    }

    void Start()
    {
        _inputHandler.SetSpeed(_currentSpeed);
    }

    void Update()
    {
        HandleMovement();
        ApplyBoundary();
    }

    public void SpeedUp(float value)
    {
        _currentSpeed += value;
        _inputHandler.SetSpeed(_currentSpeed);
    }

    private void HandleMovement()
    {
        Vector3 moveDirection = GetMoveDirection();
        float moveSpeed = CalculateMoveSpeed();

        transform.Translate(moveDirection * (moveSpeed * Time.deltaTime));
    }

    private Vector3 GetMoveDirection()
    {
        return _inputHandler.CurrentMode == PlayerInputHandler.MovementMode.Auto
            ? _autoMovement.CalculateSafestDirection(_inputHandler.CurrentSpeed)
            : _inputHandler.GetManualMoveDirection();
    }

    private float CalculateMoveSpeed()
    {
        float baseSpeed = _inputHandler.CurrentSpeed;
        return _inputHandler.IsSpeedBoostActive ? baseSpeed * _speedMul : baseSpeed;
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
        if (value > max) return max;
        if (value < min) return min;
        return value;
    }
}