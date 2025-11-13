using System;
using UnityEngine;

public class DestroyZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayerBullet>() != null ||
            other.GetComponent<EnemyBullet>() != null ||
            other.GetComponent<FollowerBulletMovement>() != null)
        {
            other.gameObject.SetActive(false);
        }
        else
        {
            Destroy(other.gameObject);
        }
    }
}
