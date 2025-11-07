using UnityEngine;

public class PlayerFireComponent : MonoBehaviour
{
    public enum FireMode
    {
        // 자동 공격
        Auto = 1,   
        // 수동 공격
        Manual = 2     
    }

    [Header("총알 프리팹")]
    [SerializeField] private GameObject _mainBulletPrefab;
    [SerializeField] private GameObject _subBulletPrefabLeft;
    [SerializeField] private GameObject _subBulletPrefabRight;

    [Header("발사 위치")]
    [SerializeField] private Transform _mainFirePositionLeft;
    [SerializeField] private Transform _mainFirePositionRight;
    [SerializeField] private Transform _subFirePositionLeft;
    [SerializeField] private Transform _subFirePositionRight;

    [Header("발사 설정")]
    [SerializeField] private float _fireCooldown = 0.6f;
    [SerializeField] private float _minFireCooldown = 0.1f;
    [SerializeField] private FireMode _currentFireMode = FireMode.Auto;

    private float _lastFireTime = -1f;

    private void Update()
    {
        HandleFireModeInput();
        HandleFiring();
    }

    private void HandleFireModeInput()
    {
        // 자동 공격
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            _currentFireMode = FireMode.Auto;
        }

        // 수동 공격
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            _currentFireMode = FireMode.Manual;
        }
    }

    private void HandleFiring()
    {
        bool shouldFire = false;

        switch (_currentFireMode)
        {
            case FireMode.Auto:
                shouldFire = true;
                break;

            case FireMode.Manual:
                shouldFire = Input.GetKey(KeyCode.Space);
                break;
        }

        if (shouldFire && CanFire())
        {
            Fire();
            _lastFireTime = Time.time;
        }
    }

    private bool CanFire()
    {
        return Time.time >= _lastFireTime + _fireCooldown;
    }

    private void Fire()
    {
        SpawnBullet(_mainBulletPrefab, _mainFirePositionLeft);
        SpawnBullet(_mainBulletPrefab, _mainFirePositionRight);
        SpawnBullet(_subBulletPrefabLeft, _subFirePositionLeft);
        SpawnBullet(_subBulletPrefabRight, _subFirePositionRight);
    }

    private void SpawnBullet(GameObject bulletPrefab, Transform firePosition)
    {
        if (bulletPrefab != null && firePosition != null)
        {
            GameObject bullet = Instantiate(bulletPrefab);
            bullet.transform.position = firePosition.position;
        }
    }

    public void IncreaseAttackSpeed(float amount)
    {
        _fireCooldown -= amount;
        _fireCooldown = Mathf.Max(_fireCooldown, _minFireCooldown);
    }
}