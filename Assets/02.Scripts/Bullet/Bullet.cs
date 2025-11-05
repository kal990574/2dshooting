using UnityEngine;

public class Bullet : MonoBehaviour
{
    public enum MovementMode
    {
        Straight = 0,      // 직선
        SideToSide = 1,    // 좌우 이동
        Rotating = 2,      // 나선형
        Bezier = 3         // 베지어 곡선
    }

    [Header("속도 설정")]
    [SerializeField] private float _startSpeed = 1f;
    [SerializeField] private float _endSpeed = 7f;
    [SerializeField] private float _accelerationTime = 1.2f;
    [SerializeField] private float _currentSpeed;

    [Header("이동 설정")]
    [SerializeField] private MovementMode _moveMode = MovementMode.Straight;

    [Header("좌우 이동 설정")]
    [SerializeField] private float _horizontalSpeed = 3f;
    [SerializeField] private float _horizontalRange = 1f;

    [Header("회전 설정")]
    [SerializeField] private float _rotationSpeed = 480f;

    [Header("베지어 곡선 설정")]
    [SerializeField] private float _bezierControlX1 = 8f;
    [SerializeField] private float _bezierControlY1 = -13f;
    [SerializeField] private float _bezierControlX2 = 2f;
    [SerializeField] private float _bezierControlY2 = 8f;
    [SerializeField] private float _bezierEndY = 20f;
    [SerializeField] private float _bezierDuration = 2.0f;

    [Header("데미지 설정")]
    [SerializeField] private float _damage = 10f;

    private float _startX;
    private float _horizontalDirection = 1f;
    // 베지어 곡선 값
    private Vector3 _bezierStartPoint;
    private Vector3 _bezierControlPoint1;
    private Vector3 _bezierControlPoint2;
    private Vector3 _bezierEndPoint;
    private float _bezierTime;

    void Start()
    {
        _currentSpeed = _startSpeed;
        _startX = transform.position.x;

        // 랜덤 부호로 베지어 곡선 방향 설정
        float randomSign = Random.value > 0.5f ? 1f : -1f;

        _bezierStartPoint = transform.position;
        _bezierControlPoint1 = new Vector3(_bezierControlX1 * randomSign, _bezierControlY1, 0f);
        _bezierControlPoint2 = new Vector3(_bezierControlX2 * randomSign, _bezierControlY2, 0f);
        _bezierEndPoint = new Vector3(0f, _bezierEndY, 0f);
        _bezierTime = 0f;
    }

    void Update()
    {
        UpdateSpeed();
        UpdateMovement();
    }

    private void UpdateSpeed()
    {
        if (_currentSpeed < _endSpeed)
        {
            float acceleration = (_endSpeed - _startSpeed) / _accelerationTime;
            _currentSpeed += acceleration * Time.deltaTime;
            _currentSpeed = Mathf.Min(_currentSpeed, _endSpeed);
        }
    }

    private void UpdateMovement()
    {
        switch (_moveMode)
        {
            case MovementMode.Straight:
                MoveStraight();
                break;
            case MovementMode.SideToSide:
                MoveSideToSide();
                break;
            case MovementMode.Rotating:
                MoveRotating();
                break;
            case MovementMode.Bezier:
                MoveBezier();
                break;
        }
    }

    private void MoveStraight()
    {
        transform.position += Vector3.up * (_currentSpeed * Time.deltaTime);
    }

    private void MoveSideToSide()
    {
        // 수직 이동
        transform.position += Vector3.up * (_currentSpeed * Time.deltaTime);

        // 수평 이동
        float horizontalMovement = _horizontalDirection * _horizontalSpeed * Time.deltaTime;
        Vector3 newPos = transform.position;
        newPos.x += horizontalMovement;

        // 방향 전환
        if (newPos.x <= _startX - _horizontalRange)
        {
            newPos.x = _startX - _horizontalRange;
            _horizontalDirection = 1f;
        }
        else if (newPos.x >= _startX + _horizontalRange)
        {
            newPos.x = _startX + _horizontalRange;
            _horizontalDirection = -1f;
        }

        transform.position = newPos;
    }

    private void MoveRotating()
    {
        transform.Translate(Vector3.up * (_currentSpeed * Time.deltaTime));
        transform.Rotate(0f, 0f, _rotationSpeed * Time.deltaTime);

        transform.position += Vector3.up * (_currentSpeed * Time.deltaTime);
    }
    
    private void MoveBezier()
    {
        if (_bezierTime <= 1f)
        {
            _bezierTime += Time.deltaTime / _bezierDuration;
            transform.position = EvaluateBezier(_bezierStartPoint, _bezierControlPoint1,
                                               _bezierControlPoint2, _bezierEndPoint, _bezierTime);
        }
    }
    
    private Vector3 EvaluateBezier(Vector3 p1, Vector3 p2, Vector3 p3, Vector3 p4, float t)
    {
        Vector3 a = Vector3.Lerp(p1, p2, t);
        Vector3 b = Vector3.Lerp(p2, p3, t);
        Vector3 c = Vector3.Lerp(p3, p4, t);
        Vector3 d = Vector3.Lerp(a, b, t);
        Vector3 e = Vector3.Lerp(b, c, t);
        return Vector3.Lerp(d, e, t);
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Hitbox"))
        {
            Hitbox hitbox = other.GetComponent<Hitbox>();
            hitbox.TakeDamage(_damage);
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}