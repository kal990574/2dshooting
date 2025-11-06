using UnityEngine;

public class StraightMovement : MovementComponent
{
    protected override void Move()
    {
        transform.Translate(Vector3.down * (_speed * Time.deltaTime));
    }
}