using UnityEngine;
using Utils;

namespace _02.Scripts.Item
{
    public class ItemMovement : MonoBehaviour
    {
        [Header("이동 설정")]
        [SerializeField] private float _waitTime = 2f;
        [SerializeField] private float _moveDuration = 1f;

        [Header("베지어 곡선 제어점")]
        [SerializeField] private Vector3 _controlPoint1Offset = new Vector3(3f, 1f, 0f);
        [SerializeField] private Vector3 _controlPoint2Offset = new Vector3(1f, 1f, 0f);

        private Vector3 _startPoint;
        private Vector3 _controlPoint1;
        private Vector3 _controlPoint2;
        private Vector3 _targetPoint;
        private float _elapsedTime;
        private bool _isMoving;
        private Transform _playerTransform;

        void Start()
        {
            _elapsedTime = 0f;
            _isMoving = false;
            _startPoint = transform.position;

            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                _playerTransform = playerObj.transform;
            }
        }

        void Update()
        {
            _elapsedTime += Time.deltaTime;

            if (!_isMoving && _elapsedTime >= _waitTime)
            {
                StartMovingToPlayer();
            }

            if (_isMoving)
            {
                MoveToPlayer();
            }
        }

        private void StartMovingToPlayer()
        {
            if (_playerTransform == null) return;

            _isMoving = true;
            _elapsedTime = 0f;
            _startPoint = transform.position;
            _targetPoint = _playerTransform.position;

            (_controlPoint1, _controlPoint2) = BezierUtility.CalculateControlPoints(
                _startPoint,
                _targetPoint,
                _controlPoint1Offset,
                _controlPoint2Offset);
        }

        private void MoveToPlayer()
        {
            if (_playerTransform == null) return;

            float t = _elapsedTime / _moveDuration;

            if (t <= 1f)
            {
                _targetPoint = _playerTransform.position;

                transform.position = BezierUtility.Evaluate(_startPoint, _controlPoint1,
                                                                  _controlPoint2, _targetPoint, t);
            }
        }
    }
}