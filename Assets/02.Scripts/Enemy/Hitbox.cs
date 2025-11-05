using UnityEngine;

public class Hitbox : MonoBehaviour
{
    [Header("데미지 배율 설정")]
    public float DamageMultiplier; 

    private Enemy _parentEnemy;

    void Start()
    {
        _parentEnemy = GetComponentInParent<Enemy>();
    }

    public void TakeDamage(float damage)
    {
        float finalDamage = damage * DamageMultiplier;
        _parentEnemy.TakeDamage(finalDamage);
    }
}