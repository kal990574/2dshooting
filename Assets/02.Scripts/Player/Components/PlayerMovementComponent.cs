using UnityEngine;

[RequireComponent(typeof(PlayerInputHandler))]
[RequireComponent(typeof(AutoMovement))]
public class PlayerMovementComponent : MonoBehaviour
{
    [Header("이동 범위")]
    private float _xMin = -4f;
    private float _xMax = 4f;
    private float _yMin = -9f;
    private float _yMax = 9f;

    [Header("속도 설정")]
    private float _currentSpeed = 4f;
    private float _speedMul = 1.5f;

    private PlayerInputHandler _inputHandler;
    private AutoMovement _autoMovement;
    private Animator _animator;

    void Awake()
    {
        _inputHandler = GetComponent<PlayerInputHandler>();
        _autoMovement = GetComponent<AutoMovement>();
        _animator = GetComponent<Animator>();
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
        UpdateAnimation(moveDirection);
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
        if (value > max) return min;
        if (value < min) return max;
        return value;
    }

    private void UpdateAnimation(Vector3 moveDirection)
    {
        //if (moveDirection.x < 0) _animator.Play("Left");
        //if (moveDirection.x == 0) _animator.Play("Idle");
        //if (moveDirection.x > 0) _animator.Play("Right");
        _animator.SetInteger("x", (int)moveDirection.x);
    }
}