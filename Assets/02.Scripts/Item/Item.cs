using System;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private float _speedIncrease = 1f;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMovementComponent playerMove = other.GetComponent<PlayerMovementComponent>();
            if (playerMove != null)
            {
                playerMove.SpeedUp(_speedIncrease);
                Destroy(gameObject);
            }
        }
    }
}
