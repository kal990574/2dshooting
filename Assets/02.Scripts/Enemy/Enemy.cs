using System;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("이동 설정")]
    public float Speed = 3f;

    [Header("체력 설정")]
    public float MaxHealth = 200f;
    public float CurrentHealth;

    void Start()
    {
        CurrentHealth = MaxHealth;
    }

    void Update()
    {
        transform.Translate(Vector3.down * (Speed * Time.deltaTime));
    }

    public void TakeDamage(float damage)
    {
        CurrentHealth -= damage;
        // Debug.Log($"Enemy HP: {CurrentHealth}/{MaxHealth}");

        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
