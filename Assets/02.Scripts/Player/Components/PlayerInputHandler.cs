using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    public enum MovementMode
    {
        Manual = 1,
        Auto = 2
    }

    [Header("속도 설정")]
    [SerializeField] private float _speedStep = 1f;

    private MovementMode _currentMovementMode = MovementMode.Manual;
    private float _currentSpeed = 4f;
    private Vector3 _originPosition;

    public MovementMode CurrentMode => _currentMovementMode;
    public float CurrentSpeed => _currentSpeed;
    public bool IsSpeedBoostActive => Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);

    void Start()
    {
        _originPosition = transform.position;
    }

    void Update()
    {
        HandleMovementModeInput();
        HandleSpeedInput();
    }

    public void SetSpeed(float speed)
    {
        _currentSpeed = speed;
    }

    public void ModifySpeed(float delta)
    {
        _currentSpeed += delta;
    }

    public Vector3 GetManualMoveDirection()
    {
        if (Input.GetKey(KeyCode.R))
        {
            return (_originPosition - transform.position).normalized;
        }

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        return new Vector2(h, v).normalized;
    }

    private void HandleMovementModeInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            _currentMovementMode = MovementMode.Manual;
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            _currentMovementMode = MovementMode.Auto;
        }
    }

    private void HandleSpeedInput()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            _currentSpeed += _speedStep;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            _currentSpeed = Mathf.Max(0f, _currentSpeed - _speedStep);
        }
    }
}