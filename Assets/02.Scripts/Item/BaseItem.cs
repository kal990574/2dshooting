using UnityEngine;

namespace _02.Scripts.Item
{
    public abstract class BaseItem : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                Player player = other.GetComponent<Player>();
                if (player != null)
                {
                    ApplyEffect(player);
                    Destroy(gameObject);
                }
            }
        }

        protected abstract void ApplyEffect(Player player);
    }
}