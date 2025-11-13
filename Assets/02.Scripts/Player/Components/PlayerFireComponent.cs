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
    
    [Header("발사 위치")]
    [SerializeField] private Transform _mainFirePositionLeft;
    [SerializeField] private Transform _mainFirePositionRight;
    [SerializeField] private Transform _subFirePositionLeft;
    [SerializeField] private Transform _subFirePositionRight;

    [Header("발사 설정")]
    [SerializeField] private float _fireCooldown = 0.6f;
    [SerializeField] private float _minFireCooldown = 0.1f;
    [SerializeField] private FireMode _currentFireMode = FireMode.Auto;

    public AudioSource FireSound;

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
        // BulletFactory를 사용해서 총알 생성
        if (_mainFirePositionLeft != null)
            BulletFactory.Instance.MakePlayerMainBullet(_mainFirePositionLeft.position);

        if (_mainFirePositionRight != null)
            BulletFactory.Instance.MakePlayerMainBullet(_mainFirePositionRight.position);

        if (_subFirePositionLeft != null)
            BulletFactory.Instance.MakePlayerSubLeftBullet(_subFirePositionLeft.position);

        if (_subFirePositionRight != null)
            BulletFactory.Instance.MakePlayerSubRightBullet(_subFirePositionRight.position);

        FireSound.Play();
    }

    public void IncreaseAttackSpeed(float amount)
    {
        _fireCooldown -= amount;
        _fireCooldown = Mathf.Max(_fireCooldown, _minFireCooldown);
    }
}