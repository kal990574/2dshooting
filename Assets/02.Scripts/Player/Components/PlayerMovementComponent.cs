using UnityEngine;

public class PlayerMovementComponent : MonoBehaviour
{
    public enum MovementMode
    {
        Manual = 1,
        Auto = 2
    }

    private const float BASE_SAFETY_SCORE = 100f;
    private const float FULL_CIRCLE_DEGREES = 360f;

    [Header("이동 범위")]
    [SerializeField] private float _xMin = -4f;
    [SerializeField] private float _xMax = 4f;
    [SerializeField] private float _yMin = -9f;
    [SerializeField] private float _yMax = 9f;

    [Header("속도 설정")]
    [SerializeField] private float _currentSpeed = 4f;
    [SerializeField] private float _speedStep = 1f;
    [SerializeField] private float _speedMul = 1.5f;

    [Header("이동 모드")]
    [SerializeField] private MovementMode _currentMovementMode = MovementMode.Manual;

    [Header("자동 이동 설정")]
    [SerializeField] private float _detectionRadius = 5f;
    [SerializeField] private float _enemyThreatWeight = 1f;
    [SerializeField] private float _bulletThreatWeight = 5f;
    [SerializeField] private int _directionSamples = 12;

    private Vector3 _originPosition;

    void Start()
    {
        _originPosition = transform.position;
    }

    void Update()
    {
        HandleMovementModeInput();
        HandleSpeedInput();
        HandleMovement();
        ApplyBoundary();
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

    public void SpeedUp(float value)
    {
        _currentSpeed += value;
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

    private void HandleMovement()
    {
        Vector3 moveDirection = GetMoveDirection();
        bool isShiftPressed = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
        float moveSpeed = isShiftPressed ? _currentSpeed * _speedMul : _currentSpeed;

        transform.Translate(moveDirection * (moveSpeed * Time.deltaTime));
    }

    private Vector3 GetMoveDirection()
    {
        return _currentMovementMode == MovementMode.Auto
            ? GetAutoMoveDirection()
            : GetManualMoveDirection();
    }

    private Vector3 GetManualMoveDirection()
    {
        if (Input.GetKey(KeyCode.R))
        {
            return (_originPosition - transform.position).normalized;
        }

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        return new Vector2(h, v).normalized;
    }

    private Vector3 GetAutoMoveDirection()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        EnemyBullet[] bullets = FindObjectsByType<EnemyBullet>(FindObjectsSortMode.None);

        if (enemies.Length == 0 && bullets.Length == 0)
        {
            return Vector3.zero;
        }

        return FindSafestDirection(enemies, bullets);
    }

    private Vector3 FindSafestDirection(GameObject[] enemies, EnemyBullet[] bullets)
    {
        float bestScore = 0f;
        Vector3 bestDirection = Vector3.zero;

        for (int i = 0; i < _directionSamples; i++)
        {
            float angle = (FULL_CIRCLE_DEGREES / _directionSamples) * i;
            Vector3 direction = Quaternion.Euler(0, 0, angle) * Vector3.up;

            float score = EvaluateDirectionSafety(direction, enemies, bullets);

            if (score > bestScore)
            {
                bestScore = score;
                bestDirection = direction;
            }
        }

        return bestDirection;
    }

    private float EvaluateDirectionSafety(Vector3 direction, GameObject[] enemies, EnemyBullet[] bullets)
    {
        Vector3 testPosition = CalculateTestPosition(direction);

        float safetyScore = BASE_SAFETY_SCORE;

        safetyScore -= CalculateEnemyThreat(testPosition, enemies);
        safetyScore -= CalculateBulletThreat(testPosition, bullets);
        safetyScore += CalculateCenterBonus(testPosition);

        return safetyScore;
    }

    private Vector3 CalculateTestPosition(Vector3 direction)
    {
        return transform.position + direction * _currentSpeed * Time.deltaTime;
    }

    private float CalculateEnemyThreat(Vector3 position, GameObject[] enemies)
    {
        float totalThreat = 0f;

        foreach (GameObject enemy in enemies)
        {
            if (enemy == null) continue;

            float threat = CalculateThreat(position, enemy.transform.position, _enemyThreatWeight);
            totalThreat += threat;
        }

        return totalThreat;
    }

    private float CalculateBulletThreat(Vector3 position, EnemyBullet[] bullets)
    {
        float totalThreat = 0f;

        foreach (EnemyBullet bullet in bullets)
        {
            if (bullet == null) continue;

            float threat = CalculateThreat(position, bullet.transform.position, _bulletThreatWeight);
            totalThreat += threat;
        }

        return totalThreat;
    }

    private float CalculateThreat(Vector3 fromPosition, Vector3 targetPosition, float threatWeight)
    {
        float distance = Vector3.Distance(fromPosition, targetPosition);

        if (distance >= _detectionRadius)
        {
            return 0f;
        }

        float normalizedDistance = distance / _detectionRadius;
        float inverseDist = 1f - normalizedDistance;

        return inverseDist * threatWeight;
    }

    private float CalculateCenterBonus(Vector3 position)
    {
        Vector3 center = _originPosition;
        
        float distanceFromCenter = Vector3.Distance(position, center);
        float maxDistance = Vector3.Distance(center, new Vector3(_xMax, _yMax, 0f));
        
        return (1f - (distanceFromCenter / maxDistance));
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