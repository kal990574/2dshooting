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
    public float StartSpeed = 1f;
    public float EndSpeed = 7f;
    public float AccelerationTime = 1.2f;
    public float CurrentSpeed;

    [Header("이동 설정")]
    public MovementMode MoveMode = MovementMode.Straight;

    [Header("좌우 이동 설정")]
    public float HorizontalSpeed = 3f;
    public float HorizontalRange = 1f;

    [Header("회전 설정")]
    public float RotationSpeed = 480f;

    [Header("데미지 설정")]
    public float Damage = 10f;

    private float _startX;
    private float _horizontalDirection = 1f;
    //bz
    private Vector3 _p1, _p2, _p3, _p4;
    private float _t;
    private float _duration = 1.0f;

    void Start()
    {
        CurrentSpeed = StartSpeed;
        _startX = transform.position.x;

        // 랜덤 
        float randomSign = Random.value > 0.5f ? 1f : -1f;

        _p1 = transform.position;
        _p2 = new Vector3(8f * randomSign, -13f, 0f);
        _p3 = new Vector3(2f * randomSign, 8f, 0f);
        _p4 = new Vector3(0f, 30f, 0f);
        _t  = 0f;
    }

    void Update()
    {
        UpdateSpeed();
        UpdateMovement();
    }

    private void UpdateSpeed()
    {
        if (CurrentSpeed < EndSpeed)
        {
            float acceleration = (EndSpeed - StartSpeed) / AccelerationTime;
            CurrentSpeed += acceleration * Time.deltaTime;
            CurrentSpeed = Mathf.Min(CurrentSpeed, EndSpeed);
        }
    }

    private void UpdateMovement()
    {
        switch (MoveMode)
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
        transform.position += Vector3.up * (CurrentSpeed * Time.deltaTime);
    }

    private void MoveSideToSide()
    {
        // 수직 이동
        transform.position += Vector3.up * (CurrentSpeed * Time.deltaTime);

        // 수평 이동
        float horizontalMovement = _horizontalDirection * HorizontalSpeed * Time.deltaTime;
        Vector3 newPos = transform.position;
        newPos.x += horizontalMovement;

        // 방향 전환
        if (newPos.x <= _startX - HorizontalRange)
        {
            newPos.x = _startX - HorizontalRange;
            _horizontalDirection = 1f;
        }
        else if (newPos.x >= _startX + HorizontalRange)
        {
            newPos.x = _startX + HorizontalRange;
            _horizontalDirection = -1f;
        }

        transform.position = newPos;
    }

    private void MoveRotating()
    {
        transform.Translate(Vector3.up * (CurrentSpeed * Time.deltaTime));
        transform.Rotate(0f, 0f, RotationSpeed * Time.deltaTime);
        
        transform.position += Vector3.up * (CurrentSpeed * Time.deltaTime);
    }
    
    private void MoveBezier()
    {
        if (_t <= 1f)
        {
            _t += Time.deltaTime / _duration;
            transform.position = EvaluateBezier(_p1, _p2, _p3, _p4, _t);
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
            hitbox.TakeDamage(Damage);
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}