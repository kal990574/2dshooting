using UnityEngine;

public class FireComponent : MonoBehaviour
{
    [Header("발사 설정")]
    [SerializeField] private GameObject _bulletPrefab;
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
        if (_bulletPrefab == null) return;

        Vector3 spawnPosition = _firePosition != null
            ? _firePosition.position
            : transform.position;

        GameObject bullet = Instantiate(_bulletPrefab);
        bullet.transform.position = spawnPosition;
    }
}