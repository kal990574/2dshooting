using UnityEngine;

public class PlayerMovementComponent : MonoBehaviour
{
    public enum MovementMode
    {
        // 수동 이동
        Manual = 1,
        // 자동 이동 (회피)
        Auto = 2
    }

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
        // 수동 이동 모드
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            _currentMovementMode = MovementMode.Manual;
        }

        // 자동 이동 모드
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
        switch (_currentMovementMode)
        {
            case MovementMode.Auto:
                return GetAutoMoveDirection();

            case MovementMode.Manual:
            default:
                return GetManualMoveDirection();
        }
    }

    private Vector3 GetManualMoveDirection()
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
        float bestScore = float.MinValue;
        Vector3 bestDirection = Vector3.zero;

        for (int i = 0; i < _directionSamples; i++)
        {
            float angle = (360f / _directionSamples) * i;
            Vector3 direction = Quaternion.Euler(0, 0, angle) * Vector3.up;

            // 방향 별 안전도 탐색
            float score = EvaluateDirectionSafety(direction, enemies, bullets);

            if (score > bestScore)
            {
                bestScore = score;
                bestDirection = direction;
            }
        }

        return bestDirection.normalized;
    }

    private float EvaluateDirectionSafety(Vector3 direction, GameObject[] enemies, EnemyBullet[] bullets)
    {
        Vector3 testPosition = transform.position + direction * _currentSpeed * Time.deltaTime;

        if (testPosition.x < _xMin || testPosition.x > _xMax ||
            testPosition.y < _yMin || testPosition.y > _yMax)
        {
            return float.MinValue;
        }

        float safetyScore = 100f;
        
        foreach (GameObject enemy in enemies)
        {
            if (enemy == null) continue;

            float distance = Vector3.Distance(testPosition, enemy.transform.position);
            
            if (distance < _detectionRadius)
            {
                float normalizedDistance = distance / _detectionRadius;
                
                float inverseDist = 1f - normalizedDistance;
                float threat = inverseDist * _enemyThreatWeight;

                safetyScore -= threat;
            }
        }

        foreach (EnemyBullet bullet in bullets)
        {
            if (bullet == null) continue;

            float distance = Vector3.Distance(testPosition, bullet.transform.position);

            if (distance < _detectionRadius)
            {
                float normalizedDistance = distance / _detectionRadius;

                float inverseDist = 1f - normalizedDistance;
                float threat = inverseDist * _bulletThreatWeight;

                safetyScore -= threat;
            }
        }

        // 중앙 선호
        float centerBonus = CalculateCenterBonus(testPosition);
        safetyScore += centerBonus;

        return safetyScore;
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