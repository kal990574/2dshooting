using UnityEngine;

public class StraightEnemy : BaseEnemy
{
    [Header("이동 설정")]
    [SerializeField] private float _speed = 3f;

    protected override void Move()
    {
        transform.Translate(Vector3.down * (_speed * Time.deltaTime));
    }
}