using System.Collections;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    [Header("공격 설정")]
    [SerializeField] private float _attackInterval = 2f;

    [Header("발사 위치")]
    [SerializeField] private Transform _firePoint;

    private Coroutine _attackCoroutine;

    private void OnEnable()
    {
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
        // 보스가 화면에 진입할 시간을 주기 위해 첫 공격은 지연
        yield return new WaitForSeconds(_attackInterval);

        while (true)
        {
            Fire3Way();
            yield return new WaitForSeconds(_attackInterval);
        }
    }

    private void Fire3Way()
    {
        Vector3 firePosition = _firePoint != null ? _firePoint.position : transform.position;

        // 중앙 발사
        FireBullet(firePosition, Vector3.down);

        // 좌측 대각선 발사
        Vector3 leftDirection = new Vector3(-1f, -1f, 0f).normalized;
        FireBullet(firePosition, leftDirection);

        // 우측 대각선 발사
        Vector3 rightDirection = new Vector3(1f, -1f, 0f).normalized;
        FireBullet(firePosition, rightDirection);
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