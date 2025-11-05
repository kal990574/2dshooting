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

    [Header("총알 프리팹")]
    public GameObject MainBulletPrefab;
    public GameObject SubBulletPrefab;

    [Header("발사 위치")]
    public Transform MainFirePositionLeft;
    public Transform MainFirePositionRight;
    public Transform SubFirePositionLeft;
    public Transform SubFirePositionRight;

    [Header("발사 설정")]
    public float FireCooldown = 0.6f;
    public FireMode CurrentFireMode = FireMode.Auto;

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
            CurrentFireMode = FireMode.Auto;
        }

        // 수동 공격
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
        return Time.time >= _lastFireTime + FireCooldown;
    }

    private void Fire()
    {
        SpawnBullet(MainBulletPrefab, MainFirePositionLeft);
        SpawnBullet(MainBulletPrefab, MainFirePositionRight);
        SpawnBullet(SubBulletPrefab, SubFirePositionLeft);
        SpawnBullet(SubBulletPrefab, SubFirePositionRight);
    }

    private void SpawnBullet(GameObject bulletPrefab, Transform firePosition)
    {
        if (bulletPrefab != null && firePosition != null)
        {
            GameObject bullet = Instantiate(bulletPrefab);
            bullet.transform.position =  firePosition.position;
        }
    }
}