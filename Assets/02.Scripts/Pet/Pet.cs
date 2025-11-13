using System.Collections.Generic;
using UnityEngine;

public class Pet : MonoBehaviour
{
    [Header("Follow Settings")]
    [SerializeField] private Transform _followTarget;
    [SerializeField] private int _followOffset = 40;
    [SerializeField] private float _minMoveDistance = 0.02f;

    private Queue<Vector3> _myTrail = new Queue<Vector3>();
    private Vector3 _lastTargetPosition;

    private void Start()
    {
        if (_followTarget != null)
        {
            _lastTargetPosition = _followTarget.position;
        }
    }

    private void Update()
    {
        FollowTrail();
    }

    private void FollowTrail()
    {
        if (_followTarget == null) return;

        float targetMovedDistance = Vector3.Distance(_followTarget.position, _lastTargetPosition);

        if (targetMovedDistance >= _minMoveDistance)
        {
            _myTrail.Enqueue(_followTarget.position);
            _lastTargetPosition = _followTarget.position;
        }

        if (_myTrail.Count > _followOffset)
        {
            transform.position = _myTrail.Dequeue();
        }
    }

}