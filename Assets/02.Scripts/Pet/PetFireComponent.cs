using UnityEngine;

public class PetFireComponent : MonoBehaviour
{
    [Header("Bullet Settings")]
    [SerializeField] private Transform _firePosition;

    [Header("Fire Settings")]
    [SerializeField] private float _fireCooldown = 1.5f; 

    private float _lastFireTime = 0f;

    private void Update()
    {
        if (CanFire())
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
        if (_firePosition != null)
        {
            // BulletFactory를 사용해서 총알 생성
            BulletFactory.Instance.MakePetBullet(_firePosition.position);
        }
    }
}