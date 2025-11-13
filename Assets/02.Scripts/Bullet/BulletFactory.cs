using UnityEngine;

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

    [Header("풀링 설정")]
    public int PlayerMainPoolSize = 30;
    public int PlayerSubPoolSize = 20;
    public int EnemyPoolSize = 50;
    public int PetPoolSize = 20;

    private GameObject[] _playerMainBulletPool;
    private GameObject[] _playerSubLeftBulletPool;
    private GameObject[] _playerSubRightBulletPool;
    private GameObject[] _enemyBulletPool;
    private GameObject[] _petBulletPool;

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
        _playerMainBulletPool = new GameObject[PlayerMainPoolSize];
        for (int i = 0; i < PlayerMainPoolSize; i++)
        {
            GameObject bulletObject = Instantiate(PlayerBulletMainPrefab, transform);
            _playerMainBulletPool[i] = bulletObject;
            bulletObject.SetActive(false);
        }

        _playerSubLeftBulletPool = new GameObject[PlayerSubPoolSize];
        for (int i = 0; i < PlayerSubPoolSize; i++)
        {
            GameObject bulletObject = Instantiate(PlayerBulletSubLeftPrefab, transform);
            _playerSubLeftBulletPool[i] = bulletObject;
            bulletObject.SetActive(false);
        }

        _playerSubRightBulletPool = new GameObject[PlayerSubPoolSize];
        for (int i = 0; i < PlayerSubPoolSize; i++)
        {
            GameObject bulletObject = Instantiate(PlayerBulletSubRightPrefab, transform);
            _playerSubRightBulletPool[i] = bulletObject;
            bulletObject.SetActive(false);
        }

        _enemyBulletPool = new GameObject[EnemyPoolSize];
        for (int i = 0; i < EnemyPoolSize; i++)
        {
            GameObject bulletObject = Instantiate(EnemyBulletPrefab, transform);
            _enemyBulletPool[i] = bulletObject;
            bulletObject.SetActive(false);
        }

        _petBulletPool = new GameObject[PetPoolSize];
        for (int i = 0; i < PetPoolSize; i++)
        {
            GameObject bulletObject = Instantiate(PetBulletPrefab, transform);
            _petBulletPool[i] = bulletObject;
            bulletObject.SetActive(false);
        }
    }

    public GameObject MakePlayerMainBullet(Vector3 position)
    {
        return GetBulletFromPool(_playerMainBulletPool, PlayerMainPoolSize, position);
    }

    public GameObject MakePlayerSubLeftBullet(Vector3 position)
    {
        return GetBulletFromPool(_playerSubLeftBulletPool, PlayerSubPoolSize, position);
    }

    public GameObject MakePlayerSubRightBullet(Vector3 position)
    {
        return GetBulletFromPool(_playerSubRightBulletPool, PlayerSubPoolSize, position);
    }

    public GameObject MakeEnemyBullet(Vector3 position)
    {
        return GetBulletFromPool(_enemyBulletPool, EnemyPoolSize, position);
    }

    public GameObject MakePetBullet(Vector3 position)
    {
        return GetBulletFromPool(_petBulletPool, PetPoolSize, position);
    }

    private GameObject GetBulletFromPool(GameObject[] pool, int poolSize, Vector3 position)
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject bulletObject = pool[i];

            if (bulletObject.activeInHierarchy == false)
            {
                bulletObject.transform.position = position;
                bulletObject.SetActive(true);
                return bulletObject;
            }
        }
        return null;
    }
}
