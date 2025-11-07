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
        [SerializeField] private Vector3 _controlPoint1Offset = new Vector3(5f, 2f, 0f);
        [SerializeField] private Vector3 _controlPoint2Offset = new Vector3(1f, 0.5f, 0f);

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

            Vector3 direction = (_targetPoint - _startPoint).normalized;
            Vector3 perpendicular = new Vector3(-direction.y, direction.x, 0f);

            _controlPoint1 = _startPoint + direction * _controlPoint1Offset.x +
                            perpendicular * _controlPoint1Offset.y +
                            Vector3.up * _controlPoint1Offset.z;

            _controlPoint2 = _targetPoint - direction * _controlPoint2Offset.x +
                            perpendicular * _controlPoint2Offset.y -
                            Vector3.up * _controlPoint2Offset.z;
        }

        private void MoveToPlayer()
        {
            if (_playerTransform == null) return;

            float t = _elapsedTime / _moveDuration;

            if (t <= 1f)
            {
                _targetPoint = _playerTransform.position;

                transform.position = BezierUtility.EvaluateCubic(_startPoint, _controlPoint1,
                                                                  _controlPoint2, _targetPoint, t);
            }
            else
            {
                transform.position = _targetPoint;
            }
        }
    }
}