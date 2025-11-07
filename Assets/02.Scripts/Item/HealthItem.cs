using UnityEngine;

namespace _02.Scripts.Item
{
    public class HealthItem : BaseItem
    {
        [Header("체력 증가량")]
        [SerializeField] private float _healthIncrease = 1f;

        protected override void ApplyEffect(Player player)
        {
            player.IncreaseHealth(_healthIncrease);
        }
    }
}