using UnityEngine;

public abstract class BulletMovementComponent : MonoBehaviour
{
    private BulletSpeedComponent _speedComponent;

    protected virtual void Awake()
    {
        _speedComponent = GetComponent<BulletSpeedComponent>();
    }

    void Update()
    {
        Move();
    }

    protected abstract void Move();

    protected float GetCurrentSpeed()
    {
        return _speedComponent != null ? _speedComponent.CurrentSpeed : 0f;
    }
}