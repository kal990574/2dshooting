using UnityEngine;

namespace _02.Scripts.Item
{
    public class AttackSpeedItem : BaseItem
    {
        [Header("공격속도 감소량")]
        [SerializeField] private float _attackSpeedIncrease = 0.05f;

        protected override void ApplyEffect(Player player)
        {
            player.IncreaseAttackSpeed(_attackSpeedIncrease);
        }
    }
}