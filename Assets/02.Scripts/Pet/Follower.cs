using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour
{
    [Header("Follow Settings")]
    [SerializeField] private Transform _followTarget;
    [SerializeField] private int _followOffset = 40;
    [SerializeField] private float _minDistance = 1f; 

    private Queue<Vector3> _myTrail = new Queue<Vector3>();

    private void Update()
    {
        FollowTrail();
    }

    private void FollowTrail()
    {
        if (_followTarget == null) return;

        float distanceToTarget = Vector3.Distance(transform.position, _followTarget.position);

        if (distanceToTarget >= _minDistance)
        {
            _myTrail.Enqueue(_followTarget.position);
        }

        if (_myTrail.Count > _followOffset)
        {
            transform.position = _myTrail.Dequeue();
        }
    }
}