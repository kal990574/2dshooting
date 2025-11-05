using System;
using UnityEngine;

public class Hitbox : MonoBehaviour
{
    [Header("데미지 배율 설정")]
    public float DamageMultiplier = 1f; 

    [Header("히트박스 정보")]
    public string HitboxName = "Front";

    private Enemy _parentEnemy;

    void Start()
    {
        _parentEnemy = GetComponentInParent<Enemy>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            Bullet bullet = other.GetComponent<Bullet>();

            // 데미지 배율 적용
            float finalDamage = bullet.Damage * DamageMultiplier;
            _parentEnemy.TakeDamage(finalDamage);

            // 디버깅
            Debug.Log($"{HitboxName} 히트! 배율: {DamageMultiplier}x, 최종 데미지: {finalDamage}");
        }
    }
}