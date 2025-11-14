using UnityEngine;

public class Hitbox : MonoBehaviour
{
    [Header("데미지 배율 설정")]
    [SerializeField] private float _damageMultiplier;

    [Header("크리티컬 설정")]
    [SerializeField] private float _criticalChance = 30f;
    [SerializeField] private float _criticalMultiplier = 1.5f;

    private Enemy _parentEnemy;

    void OnEnable()
    {
        _parentEnemy = GetComponentInParent<Enemy>();
    }

    public void TakeDamage(float damage)
    {
        float finalDamage = damage * _damageMultiplier;
        bool isCritical = IsCritical();

        if (_parentEnemy != null)
        {
            if (isCritical)
            {
                finalDamage *= _criticalMultiplier;
                _parentEnemy.ApplyKnockback();
            }

            _parentEnemy.TakeDamage(finalDamage);
        }
    }

    private bool IsCritical()
    {
        return Random.Range(0f, 100f) < _criticalChance;
    }
}