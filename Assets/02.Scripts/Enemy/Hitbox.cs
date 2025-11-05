using UnityEngine;

public class Hitbox : MonoBehaviour
{
    [Header("데미지 배율 설정")]
    public float DamageMultiplier;

    [Header("크리티컬 설정")]
    private float CriticalChance = 10f;
    private float CriticalMultiplier = 2.0f;

    private Enemy _parentEnemy;

    void Start()
    {
        _parentEnemy = GetComponentInParent<Enemy>();
    }

    public void TakeDamage(float damage)
    {
        float finalDamage = damage * DamageMultiplier;
        bool isCritical = IsCritical();

        if (_parentEnemy != null)
        {
            if (isCritical)
            {
                finalDamage *= CriticalMultiplier;
                _parentEnemy.ApplyKnockback();
            }
            
            _parentEnemy.TakeDamage(finalDamage);
        }
    }

    private bool IsCritical()
    {
        return Random.Range(0f, 100f) < CriticalChance;
    }
}