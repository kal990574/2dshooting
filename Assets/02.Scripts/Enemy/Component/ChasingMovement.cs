using UnityEngine;

public class ChasingMovement : MovementComponent
{
    private Transform _playerTransform;

    void Start()
    {
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