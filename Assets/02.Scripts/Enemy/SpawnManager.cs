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

    private float _spawnTimer = 0f;

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
        // 스폰할 프리팹 선택 
        GameObject prefabToSpawn = Random.Range(0f, 100f) < _straightEnemyChance
            ? _straightEnemyPrefab
            : _chasingEnemyPrefab;

        if (prefabToSpawn == null) return;

        // 랜덤 위치 생성
        float randomX = Random.Range(_spawnXMin, _spawnXMax);
        Vector3 spawnPosition = new Vector3(randomX, _spawnYPosition, 0f);

        // 적 생성
        GameObject enemy = Instantiate(prefabToSpawn);
        enemy.transform.position = spawnPosition;
    }
}