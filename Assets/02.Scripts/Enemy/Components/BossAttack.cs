using System.Collections;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    [Header("공격 설정")]
    [SerializeField] private float _attackInterval = 2f;

    [Header("발사 위치")]
    [SerializeField] private Transform _firePoint;

    [Header("Circle Shot 설정")]
    [SerializeField] private int _circleBulletCount = 12;
    [SerializeField] private float _rotationOffset = 0f;

    [Header("페이즈 설정")]
    [SerializeField] private float _phase2HealthThreshold = 0.5f;
    [SerializeField] private float _phase2AttackDelay = 0.5f;

    private float _currentRotation = 0f;
    private HealthComponent _healthComponent;
    private Coroutine _attackCoroutine;

    private void OnEnable()
    {
        if (_healthComponent == null)
        {
            _healthComponent = GetComponent<HealthComponent>();
        }
        StartAttack();
    }

    private void OnDisable()
    {
        StopAttack();
    }

    private void StartAttack()
    {
        if (_attackCoroutine == null)
        {
            _attackCoroutine = StartCoroutine(AttackRoutine());
        }
    }

    private void StopAttack()
    {
        if (_attackCoroutine != null)
        {
            StopCoroutine(_attackCoroutine);
            _attackCoroutine = null;
        }
    }

    private IEnumerator AttackRoutine()
    {
        yield return new WaitForSeconds(_attackInterval);

        while (true)
        {
            if (_healthComponent != null && _healthComponent.HealthPercentage <= _phase2HealthThreshold)
            {
                FireCircle();
                yield return new WaitForSeconds(_phase2AttackDelay);
                Fire3Way();
            }
            else
            {
                Fire3Way();
            }

            yield return new WaitForSeconds(_attackInterval);
        }
    }

    private void Fire3Way()
    {
        Vector3 firePosition = _firePoint != null ? _firePoint.position : transform.position;

        FireBullet(firePosition, Vector3.down);

        Vector3 leftDirection = new Vector3(-1f, -1f, 0f).normalized;
        FireBullet(firePosition, leftDirection);

        Vector3 rightDirection = new Vector3(1f, -1f, 0f).normalized;
        FireBullet(firePosition, rightDirection);
    }

    private void FireCircle()
    {
        Vector3 firePosition = _firePoint != null ? _firePoint.position : transform.position;

        float angleStep = 360f / _circleBulletCount;

        for (int i = 0; i < _circleBulletCount; i++)
        {
            float angle = (angleStep * i) + _currentRotation;
            float angleInRadians = angle * Mathf.Deg2Rad;

            Vector3 direction = new Vector3(
                Mathf.Cos(angleInRadians),
                Mathf.Sin(angleInRadians),
                0f
            ).normalized;

            FireBullet(firePosition, direction);
        }

        _currentRotation += _rotationOffset;
        if (_currentRotation >= 360f)
        {
            _currentRotation -= 360f;
        }
    }

    private void FireBullet(Vector3 position, Vector3 direction)
    {
        GameObject bullet = BulletFactory.Instance.MakeBossBullet(position);

        if (bullet != null)
        {
            BossBulletMovement movement = bullet.GetComponent<BossBulletMovement>();
            if (movement != null)
            {
                movement.SetDirection(direction);
            }
        }
        else
        {
            Debug.LogWarning("보스 총알 풀이 부족합니다!");
        }
    }
}