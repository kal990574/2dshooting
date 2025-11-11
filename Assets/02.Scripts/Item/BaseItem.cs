using UnityEngine;
using UnityEngine.Serialization;

namespace _02.Scripts.Item
{
    public abstract class BaseItem : MonoBehaviour
    {
        [Header("파티클 효과")]
        public GameObject ParticleEffectPrefab;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                Player player = other.GetComponent<Player>();
                if (player != null)
                {
                    ApplyEffect(player);
                    SpawnParticleEffect(player);
                    Destroy(gameObject);
                }
            }
        }

        protected abstract void ApplyEffect(Player player);

        private void SpawnParticleEffect(Player player)
        {
            if (ParticleEffectPrefab != null)
            {
                GameObject particle = Instantiate(ParticleEffectPrefab, player.transform.position, Quaternion.identity);
            }
        }
    }
}