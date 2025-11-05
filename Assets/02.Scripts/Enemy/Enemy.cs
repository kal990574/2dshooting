using System;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("이동 설정")]
    public float Speed = 3f;

    [Header("체력 설정")]
    public float Health = 100f;

    void Update()
    {
        transform.Translate(Vector3.down * (Speed * Time.deltaTime));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            Bullet bullet = other.GetComponent<Bullet>();
            Health -= bullet.Damage;
            if (Health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
