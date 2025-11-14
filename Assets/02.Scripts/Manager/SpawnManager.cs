using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [Header("스폰 설정")]
    [SerializeField] private GameObject _straightEnemyPrefab;
    [SerializeField] private GameObject _chasingEnemyPrefab;
    [SerializeField] private float _spawnInterval = 0.1f;

    [Header("스폰 확률 설정")]
    [SerializeField] private float _straightEnemyChance = 70f;

    [Header("스폰 포지션")]
    [SerializeField] private float _spawnYPosition = 10f;
    [SerializeField] private float _spawnXMin = -4f;
    [SerializeField] private float _spawnXMax = 4f;

    [Header("보스 스폰 포지션")]
    [SerializeField] private float _bossSpawnYPosition = 8f;
    [SerializeField] private float _bossSpawnXPosition = 0f;

    private float _spawnTimer = 0f;
    private ScoreManager _scoreManager;

    void Start()
    {
        _scoreManager = FindAnyObjectByType<ScoreManager>();
        if (_scoreManager != null)
        {
            _scoreManager.OnBossSpawn += SpawnBoss;
        }
    }

    void OnDestroy()
    {
        if (_scoreManager != null)
        {
            _scoreManager.OnBossSpawn -= SpawnBoss;
        }
    }

    void Update()
    {
        _spawnTimer += Time.deltaTime;

        if (_spawnTimer >= _spawnInterval)
        {
            SpawnEnemy();
            _spawnTimer = 0f;
            _spawnInterval = Random.Range(0.1f, 1f);
        }
    }

    public void SpawnEnemy()
    {
        float randomX = Random.Range(_spawnXMin, _spawnXMax);
        Vector3 spawnPosition = new Vector3(randomX, _spawnYPosition, 0f);

        if (Random.Range(0f, 100f) < _straightEnemyChance)
        {
            EnemyFactory.Instance.MakeStraightEnemy(spawnPosition);
        }
        else
        {
            EnemyFactory.Instance.MakeChasingEnemy(spawnPosition);
        }
    }

    private void SpawnBoss()
    {
        Vector3 bossSpawnPosition = new Vector3(_bossSpawnXPosition, _bossSpawnYPosition, 0f);
        GameObject boss = EnemyFactory.Instance.MakeBoss(bossSpawnPosition);

        if (boss != null)
        {
            Debug.Log("보스 스폰 완료!");
        }
        else
        {
            Debug.LogWarning("보스 Pool이 부족합니다!");
        }
    }
}