using UnityEngine;

public class Boss : Enemy
{
    protected override void Awake()
    {
        base.Awake();
        _score = 1000000;
    }

    protected override void Die()
    {
        MakeExplosionEffect();

        ScoreManager scoreManager = FindAnyObjectByType<ScoreManager>();
        scoreManager.AddScore(_score);
        gameObject.SetActive(false);
    }
}