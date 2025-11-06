using UnityEngine;

public class BezierBulletMovement : BulletMovementComponent
{
    [Header("베지어 곡선 설정")]
    [SerializeField] private float _bezierControlX1 = 8f;
    [SerializeField] private float _bezierControlY1 = -13f;
    [SerializeField] private float _bezierControlX2 = 2f;
    [SerializeField] private float _bezierControlY2 = 8f;
    [SerializeField] private float _bezierEndY = 20f;
    [SerializeField] private float _bezierDuration = 2.0f;

    private Vector3 _bezierStartPoint;
    private Vector3 _bezierControlPoint1;
    private Vector3 _bezierControlPoint2;
    private Vector3 _bezierEndPoint;
    private float _bezierTime;

    protected override void Start()
    {
        base.Start();

        // 랜덤 부호로 베지어 곡선 방향 설정
        float randomSign = Random.value > 0.5f ? 1f : -1f;

        _bezierStartPoint = transform.position;
        _bezierControlPoint1 = new Vector3(_bezierControlX1 * randomSign, _bezierControlY1, 0f);
        _bezierControlPoint2 = new Vector3(_bezierControlX2 * randomSign, _bezierControlY2, 0f);
        _bezierEndPoint = new Vector3(0f, _bezierEndY, 0f);
        _bezierTime = 0f;
    }

    protected override void Move()
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
}