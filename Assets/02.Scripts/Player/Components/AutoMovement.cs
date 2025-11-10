using UnityEngine;

public class AutoMovement : MonoBehaviour
{
    private const float BASE_SAFETY_SCORE = 100f;
    private const float FULL_CIRCLE_DEGREES = 360f;

    [Header("탐지 설정")]
    [SerializeField] private float _detectionRadius = 5f;
    [SerializeField] private int _directionSamples = 12;

    [Header("위협 가중치")]
    [SerializeField] private float _enemyThreatWeight = 1f;
    [SerializeField] private float _bulletThreatWeight = 5f;

    [Header("경계 설정")]
    [SerializeField] private float _xMax = 4f;
    [SerializeField] private float _yMax = 9f;

    private Vector3 _originPosition;

    void Start()
    {
        _originPosition = transform.position;
    }

    public Vector3 CalculateSafestDirection(float speed)
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        EnemyBullet[] bullets = FindObjectsByType<EnemyBullet>(FindObjectsSortMode.None);

        if (enemies.Length == 0 && bullets.Length == 0)
        {
            return Vector3.zero;
        }

        return FindSafestDirection(enemies, bullets, speed);
    }

    private Vector3 FindSafestDirection(GameObject[] enemies, EnemyBullet[] bullets, float speed)
    {
        float bestScore = 0f;
        Vector3 bestDirection = Vector3.zero;

        for (int i = 0; i < _directionSamples; i++)
        {
            float angle = (FULL_CIRCLE_DEGREES / _directionSamples) * i;
            Vector3 direction = Quaternion.Euler(0, 0, angle) * Vector3.up;

            float score = EvaluateDirectionSafety(direction, enemies, bullets, speed);

            if (score > bestScore)
            {
                bestScore = score;
                bestDirection = direction;
            }
        }

        return bestDirection;
    }

    private float EvaluateDirectionSafety(Vector3 direction, GameObject[] enemies, EnemyBullet[] bullets, float speed)
    {
        Vector3 testPosition = CalculateTestPosition(direction, speed);

        float safetyScore = BASE_SAFETY_SCORE;

        safetyScore -= CalculateEnemyThreat(testPosition, enemies);
        safetyScore -= CalculateBulletThreat(testPosition, bullets);
        safetyScore += CalculateCenterBonus(testPosition);

        return safetyScore;
    }

    private Vector3 CalculateTestPosition(Vector3 direction, float speed)
    {
        return transform.position + direction * speed * Time.deltaTime;
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
}