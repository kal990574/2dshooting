using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    public enum FireMode
    {
        // 자동 공격
        Auto = 1,   
        // 수동 공격
        Manual = 2     
    }

    [Header("Bullet Prefabs")]
    public GameObject MainBulletPrefab;
    public GameObject SubBulletPrefab;

    [Header("Fire Positions")]
    public Transform MainFirePositionLeft;
    public Transform MainFirePositionRight;
    public Transform SubFirePositionLeft;
    public Transform SubFirePositionRight;

    [Header("Fire Settings")]
    public float FireCooldown = 0.6f;
    public FireMode CurrentFireMode = FireMode.Auto;

    // Private variables
    private float _lastFireTime = -1f;

    private void Update()
    {
        HandleFireModeInput();
        HandleFiring();
    }

    private void HandleFireModeInput()
    {
        // 1: 자동 공격 모드
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            CurrentFireMode = FireMode.Auto;
        }

        // 2: 수동 공격 모드
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            CurrentFireMode = FireMode.Manual;
        }
    }

    private void HandleFiring()
    {
        bool shouldFire = false;

        switch (CurrentFireMode)
        {
            case FireMode.Auto:
                // 자동 공격: 쿨다운마다 자동 발사
                shouldFire = true;
                break;

            case FireMode.Manual:
                // 수동 공격: Space를 누르고 있을 때만 발사
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
        return Time.time >= _lastFireTime + FireCooldown;
    }

    private void Fire()
    {
        // Main 총알 발사 (좌우)
        SpawnBullet(MainBulletPrefab, MainFirePositionLeft);
        SpawnBullet(MainBulletPrefab, MainFirePositionRight);

        // Sub 총알 발사 (좌우)
        SpawnBullet(SubBulletPrefab, SubFirePositionLeft);
        SpawnBullet(SubBulletPrefab, SubFirePositionRight);
    }

    private void SpawnBullet(GameObject bulletPrefab, Transform firePosition)
    {
        if (bulletPrefab != null && firePosition != null)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePosition.position, Quaternion.identity);
        }
    }
}