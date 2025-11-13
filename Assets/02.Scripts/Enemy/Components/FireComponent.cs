using UnityEngine;

public class FireComponent : MonoBehaviour
{
    [Header("발사 설정")]
    [SerializeField] private Transform _firePosition;
    [SerializeField] private float _fireInterval = 1.5f;
    [SerializeField] private bool _autoFire = true;

    private float _lastFireTime = -999f;

    void Update()
    {
        if (_autoFire)
        {
            TryFire();
        }
    }

    public void TryFire()
    {
        if (CanFire())
        {
            Fire();
            _lastFireTime = Time.time;
        }
    }

    private bool CanFire()
    {
        return Time.time >= _lastFireTime + _fireInterval;
    }

    private void Fire()
    {
        Vector3 spawnPosition = _firePosition != null
            ? _firePosition.position
            : transform.position;

        // BulletFactory를 사용해서 총알 생성
        BulletFactory.Instance.MakeEnemyBullet(spawnPosition);
    }
}