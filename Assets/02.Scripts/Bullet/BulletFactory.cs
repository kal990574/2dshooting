using UnityEngine;
using System.Collections.Generic;

public class BulletFactory : MonoBehaviour
{
    private static BulletFactory _instance = null;
    public static BulletFactory Instance => _instance;

    [Header("Player 총알 프리팹")]
    public GameObject PlayerBulletMainPrefab;
    public GameObject PlayerBulletSubLeftPrefab;
    public GameObject PlayerBulletSubRightPrefab;

    [Header("Enemy 총알 프리팹")]
    public GameObject EnemyBulletPrefab;

    [Header("Pet 총알 프리팹")]
    public GameObject PetBulletPrefab;

    [Header("Boss 총알 프리팹")]
    public GameObject BossBulletPrefab;

    [Header("풀링 설정")]
    public int PlayerMainPoolSize = 30;
    public int PlayerSubPoolSize = 20;
    public int EnemyPoolSize = 50;
    public int PetPoolSize = 20;
    public int BossPoolSize = 30;

    private List<GameObject> _playerMainBulletPool;
    private List<GameObject> _playerSubLeftBulletPool;
    private List<GameObject> _playerSubRightBulletPool;
    private List<GameObject> _enemyBulletPool;
    private List<GameObject> _petBulletPool;
    private List<GameObject> _bossBulletPool;

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(this.gameObject);
            return;
        }

        _instance = this;
        PoolInit();
    }

    private void PoolInit()
    {
        _playerMainBulletPool = CreatePool(PlayerBulletMainPrefab, PlayerMainPoolSize);
        _playerSubLeftBulletPool = CreatePool(PlayerBulletSubLeftPrefab, PlayerSubPoolSize);
        _playerSubRightBulletPool = CreatePool(PlayerBulletSubRightPrefab, PlayerSubPoolSize);
        _enemyBulletPool = CreatePool(EnemyBulletPrefab, EnemyPoolSize);
        _petBulletPool = CreatePool(PetBulletPrefab, PetPoolSize);
        _bossBulletPool = CreatePool(BossBulletPrefab, BossPoolSize);
    }

    private List<GameObject> CreatePool(GameObject prefab, int poolSize)
    {
        List<GameObject> pool = new List<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            GameObject bulletObject = Instantiate(prefab, transform);
            bulletObject.SetActive(false);
            pool.Add(bulletObject);
        }

        return pool;
    }

    public GameObject MakePlayerMainBullet(Vector3 position)
    {
        return GetBulletFromPool(_playerMainBulletPool, position);
    }

    public GameObject MakePlayerSubLeftBullet(Vector3 position)
    {
        return GetBulletFromPool(_playerSubLeftBulletPool, position);
    }

    public GameObject MakePlayerSubRightBullet(Vector3 position)
    {
        return GetBulletFromPool(_playerSubRightBulletPool, position);
    }

    public GameObject MakeEnemyBullet(Vector3 position)
    {
        return GetBulletFromPool(_enemyBulletPool, position);
    }

    public GameObject MakePetBullet(Vector3 position)
    {
        return GetBulletFromPool(_petBulletPool, position);
    }

    public GameObject MakeBossBullet(Vector3 position)
    {
        return GetBulletFromPool(_bossBulletPool, position);
    }

    private GameObject GetBulletFromPool(List<GameObject> pool, Vector3 position)
    {
        for (int i = 0; i < pool.Count; i++)
        {
            if (!pool[i].activeInHierarchy)
            {
                GameObject bulletObject = pool[i];
                bulletObject.transform.position = position;
                bulletObject.SetActive(true);
                return bulletObject;
            }
        }

        return null;
    }
}
