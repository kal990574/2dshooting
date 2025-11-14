using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory : MonoBehaviour
{
    private static EnemyFactory _instance = null;
    public static EnemyFactory Instance => _instance;

    [Header("Enemy 프리팹")]
    public GameObject StraightEnemyPrefab;
    public GameObject ChasingEnemyPrefab;

    [Header("Boss 프리팹")]
    public GameObject BossPrefab;

    [Header("풀링 설정")]
    public int StraightEnemyPoolSize = 50;
    public int ChasingEnemyPoolSize = 50;
    public int BossPoolSize = 3;

    private List<GameObject> _straightEnemyPool;
    private List<GameObject> _chasingEnemyPool;
    private List<GameObject> _bossPool;

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
        _straightEnemyPool = new List<GameObject>();
        for (int i = 0; i < StraightEnemyPoolSize; i++)
        {
            GameObject enemyObject = Instantiate(StraightEnemyPrefab, transform);
            _straightEnemyPool.Add(enemyObject);
            enemyObject.SetActive(false);
        }

        _chasingEnemyPool = new List<GameObject>();
        for (int i = 0; i < ChasingEnemyPoolSize; i++)
        {
            GameObject enemyObject = Instantiate(ChasingEnemyPrefab, transform);
            _chasingEnemyPool.Add(enemyObject);
            enemyObject.SetActive(false);
        }

        _bossPool = new List<GameObject>();
        for (int i = 0; i < BossPoolSize; i++)
        {
            GameObject bossObject = Instantiate(BossPrefab, transform);
            _bossPool.Add(bossObject);
            bossObject.SetActive(false);
        }
    }

    public GameObject MakeStraightEnemy(Vector3 position)
    {
        return GetEnemyFromPool(_straightEnemyPool, position);
    }

    public GameObject MakeChasingEnemy(Vector3 position)
    {
        return GetEnemyFromPool(_chasingEnemyPool, position);
    }

    public GameObject MakeBoss(Vector3 position)
    {
        return GetEnemyFromPool(_bossPool, position);
    }

    private GameObject GetEnemyFromPool(List<GameObject> pool, Vector3 position)
    {
        foreach (GameObject enemyObject in pool)
        {
            if (!enemyObject.activeInHierarchy)
            {
                enemyObject.transform.position = position;
                enemyObject.SetActive(true);
                return enemyObject;
            }
        }
        return null;
    }
}