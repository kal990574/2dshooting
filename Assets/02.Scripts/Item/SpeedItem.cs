using UnityEngine;

namespace _02.Scripts.Item
{
    public class SpeedItem : BaseItem
    {
        [Header("속도 증가량")]
        [SerializeField] private float _speedIncrease = 1f;

        protected override void ApplyEffect(Player player)
        {
            player.IncreaseSpeed(_speedIncrease);
        }
    }
}