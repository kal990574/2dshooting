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
}