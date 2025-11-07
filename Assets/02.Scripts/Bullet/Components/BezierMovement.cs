using UnityEngine;
using Utils;

public class BezierMovement : BulletMovementComponent
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
            transform.position = BezierUtility.EvaluateCubic(_bezierStartPoint, _bezierControlPoint1,
                                                              _bezierControlPoint2, _bezierEndPoint, _bezierTime);
        }
    }
}