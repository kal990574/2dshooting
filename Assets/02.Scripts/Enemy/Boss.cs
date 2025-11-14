using UnityEngine;

public class Boss : Enemy
{
    protected override void Awake()
    {
        base.Awake();
        _score = 1000000; // 100만점
    }

    protected override void Die()
    {
        // 보스는 아이템 드롭 없음
        MakeExplosionEffect();

        ScoreManager scoreManager = FindAnyObjectByType<ScoreManager>();
        scoreManager.AddScore(_score);
        gameObject.SetActive(false);
    }
}