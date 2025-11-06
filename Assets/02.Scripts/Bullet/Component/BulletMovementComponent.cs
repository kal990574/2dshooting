using UnityEngine;

public abstract class BulletMovementComponent : MonoBehaviour
{
    protected BulletSpeedComponent _speedComponent;

    protected virtual void Start()
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