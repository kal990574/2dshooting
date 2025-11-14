using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory : MonoBehaviour
{
    private static EnemyFactory _instance = null;
    public static EnemyFactory Instance => _instance;

    [Header("Enemy 프리팹")]
    public GameObject StraightEnemyPrefab;
    public GameObject ChasingEnemyPrefab;

    [Header("풀링 설정")]
    public int StraightEnemyPoolSize = 30;
    public int ChasingEnemyPoolSize = 30;

    private List<GameObject> _straightEnemyPool;
    private List<GameObject> _chasingEnemyPool;

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
        // Straight Enemy Pool
        _straightEnemyPool = new List<GameObject>();
        for (int i = 0; i < StraightEnemyPoolSize; i++)
        {
            GameObject enemyObject = Instantiate(StraightEnemyPrefab, transform);
            _straightEnemyPool.Add(enemyObject);
            enemyObject.SetActive(false);
        }

        // Chasing Enemy Pool
        _chasingEnemyPool = new List<GameObject>();
        for (int i = 0; i < ChasingEnemyPoolSize; i++)
        {
            GameObject enemyObject = Instantiate(ChasingEnemyPrefab, transform);
            _chasingEnemyPool.Add(enemyObject);
            enemyObject.SetActive(false);
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