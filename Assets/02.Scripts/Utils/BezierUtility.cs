using UnityEngine;

namespace Utils
{
    public static class BezierUtility
    {
        public static Vector3 Evaluate(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
        {
            Vector3 a = Vector3.Lerp(p0, p1, t);
            Vector3 b = Vector3.Lerp(p1, p2, t);
            Vector3 c = Vector3.Lerp(p2, p3, t);
            Vector3 d = Vector3.Lerp(a, b, t);
            Vector3 e = Vector3.Lerp(b, c, t);
            return Vector3.Lerp(d, e, t);
        }

        public static (Vector3 controlPoint1, Vector3 controlPoint2) CalculateControlPoints(
            Vector3 startPoint,
            Vector3 endPoint,
            Vector3 controlPoint1Offset,
            Vector3 controlPoint2Offset)
        {
            Vector3 controlPoint1 = new Vector3(startPoint.x + controlPoint1Offset.x, startPoint.y + controlPoint1Offset.y, 0);
            Vector3 controlPoint2 = new Vector3(endPoint.x + controlPoint2Offset.x, endPoint.y + controlPoint2Offset.y, 0);
            
            return (controlPoint1, controlPoint2);
        }
    }
}