using UnityEngine;
using Utils;

public class BezierMovement : BulletMovementComponent
{
    [Header("베지어 곡선 설정")]
    [SerializeField] private Vector3 _endPointPosition = new Vector3(0f, 20f, 0f);
    [SerializeField] private Vector3 _controlPoint1Offset = new Vector3(5f, -8f, 0f);
    [SerializeField] private Vector3 _controlPoint2Offset = new Vector3(2f, 0f, 0f);
    [SerializeField] private float _bezierDuration = 2.0f;
    [SerializeField] private float _bezierDirection = 0f;

    private Vector3 _bezierStartPoint;
    private Vector3 _bezierControlPoint1;
    private Vector3 _bezierControlPoint2;
    private Vector3 _bezierEndPoint;
    private float _bezierTime;

    private void OnEnable()
    {
        _bezierStartPoint = transform.position;
        _bezierEndPoint = _endPointPosition;
        _bezierTime = 0f;

        Vector3 controlOffset1 = _controlPoint1Offset;
        Vector3 controlOffset2 = _controlPoint2Offset;
        controlOffset1.x *= _bezierDirection;
        controlOffset2.x *= _bezierDirection;

        (_bezierControlPoint1, _bezierControlPoint2) = BezierUtility.CalculateControlPoints(
            _bezierStartPoint,
            _bezierEndPoint,
            controlOffset1,
            controlOffset2);
    }

    protected override void Move()
    {
        if (_bezierTime <= 1f)
        {
            _bezierTime += Time.deltaTime / _bezierDuration;
            transform.position = BezierUtility.Evaluate(_bezierStartPoint, _bezierControlPoint1,
                                                              _bezierControlPoint2, _bezierEndPoint, _bezierTime);
        }
    }
}