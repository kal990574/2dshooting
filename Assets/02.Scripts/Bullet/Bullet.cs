using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Speed Settings")]
    public float StartSpeed = 1f;        
    public float EndSpeed = 7f;         
    public float AccelerationTime = 1.2f;
    public float _currentSpeed;
    private float _elapsedTime = 0f;

    void Start()
    {
        _currentSpeed = StartSpeed;
    }

    void Update()
    {
        // 가속
        if (_elapsedTime < AccelerationTime)
        {
            _elapsedTime += Time.deltaTime;
            float t = _elapsedTime / AccelerationTime;
            _currentSpeed = Mathf.Lerp(StartSpeed, EndSpeed, t);
        }
        else
        {
            _currentSpeed = EndSpeed;
        }

        // 총알 이동
        transform.Translate(Vector2.up * (Time.deltaTime * _currentSpeed));
    }
}
