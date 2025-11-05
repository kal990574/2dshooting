using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [Header("스폰 설정")]
    public GameObject EnemyPrefab;
    public float SpawnInterval = 0.1f;

    [Header("스폰 포지션")]
    public float SpawnYPosition = 10f;
    public float SpawnXMin = -4f;
    public float SpawnXMax = 4f;

    private float _spawnTimer = 0f;

    void Update()
    {
        _spawnTimer += Time.deltaTime;

        if (_spawnTimer >= SpawnInterval)
        {
            SpawnEnemy();
            _spawnTimer = 0f;
            SpawnInterval = Random.Range(0.1f, 1f);
        }
    }

    public void SpawnEnemy()
    {
        if (EnemyPrefab == null) return;

        float randomX = Random.Range(SpawnXMin, SpawnXMax);
        Vector3 spawnPosition = new Vector3(randomX, SpawnYPosition, 0f);

        GameObject enemy = Instantiate(EnemyPrefab);
        enemy.transform.position = spawnPosition;
    }
}