using UnityEngine;

public class ChasingEnemy : BaseEnemy
{
    [Header("이동 설정")]
    [SerializeField] private float _speed = 3f;

    private Transform _playerTransform;

    protected override void Start()
    {
        base.Start();
        
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            _playerTransform = player.transform;
        }
    }

    protected override void Move()
    {
        if (_playerTransform == null) return;
        
        Vector3 direction = (_playerTransform.position - transform.position).normalized;
        transform.Translate(direction * (_speed * Time.deltaTime));
    }
}