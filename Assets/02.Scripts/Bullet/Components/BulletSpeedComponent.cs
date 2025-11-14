using UnityEngine;

public class BulletSpeedComponent : MonoBehaviour
{
    [Header("속도 설정")]
    [SerializeField] private float _startSpeed = 3f;
    [SerializeField] private float _endSpeed = 7f;
    [SerializeField] private float _accelerationTime = 1.2f;

    private float _currentSpeed;

    public float CurrentSpeed => _currentSpeed;

    void OnEnable()
    {
        _currentSpeed = _startSpeed;
    }

    void Update()
    {
        UpdateSpeed();
    }

    private void UpdateSpeed()
    {
        if (_currentSpeed < _endSpeed)
        {
            float acceleration = (_endSpeed - _startSpeed) / _accelerationTime;
            _currentSpeed += acceleration * Time.deltaTime;
            _currentSpeed = Mathf.Min(_currentSpeed, _endSpeed);
        }
    }
}