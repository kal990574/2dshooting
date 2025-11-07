using UnityEngine;

namespace _02.Scripts.Item
{
    public class ItemDropper : MonoBehaviour
    {
        [Header("아이템 프리팹")]
        [SerializeField] private GameObject _healthItemPrefab;
        [SerializeField] private GameObject _speedItemPrefab;
        [SerializeField] private GameObject _attackSpeedItemPrefab;

        [Header("드랍 확률 설정")]
        [SerializeField] private float _dropChance = 50f;

        [Header("아이템 타입별 확률")]
        [SerializeField] private float _healthItemChance = 70f;      
        [SerializeField] private float _speedItemChance = 20f; 

        public void TryDropItem()
        {
            if (Random.Range(0f, 100f) > _dropChance)
            {
                return;
            }
            
            GameObject itemToSpawn = SelectItemType();

            if (itemToSpawn != null)
            {
                Instantiate(itemToSpawn, transform.position, Quaternion.identity);
            }
        }

        private GameObject SelectItemType()
        {
            float randomValue = Random.Range(0f, 100f);
            float itemProbability = 0f;

            
            itemProbability += _healthItemChance;
            if (randomValue < itemProbability)
            {
                return _healthItemPrefab;
            }

            
            itemProbability += _speedItemChance;
            if (randomValue < itemProbability)
            {
                return _speedItemPrefab;
            }
            
            return _attackSpeedItemPrefab;
        }
    }
}