using UnityEngine;

public class StraightBulletMovement : BulletMovementComponent
{
    protected override void Move()
    {
        transform.position += Vector3.up * (GetCurrentSpeed() * Time.deltaTime);
    }
}