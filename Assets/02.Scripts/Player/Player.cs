using UnityEngine;

public class Player : MonoBehaviour
{
    private HealthComponent _healthComponent;

    void Awake()
    {
        _healthComponent = GetComponent<HealthComponent>();
    }

    public void TakeDamage(float damage)
    {
        if (_healthComponent != null)
        {
            _healthComponent.TakeDamage(damage);
        }
    }
}