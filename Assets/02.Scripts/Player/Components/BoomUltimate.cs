using System.Collections;
using UnityEngine;

public class BoomUltimate : MonoBehaviour
{
    [Header("붐 스프라이트 설정")]
    [SerializeField] private GameObject _boomSprite;

    [Header("파티클 설정")]
    [SerializeField] private GameObject _particleEffectPrefab;
    [SerializeField] private float _particleDuration = 3f;

    private bool _isActive = false;

    public void ActivateBoom()
    {
        if (_isActive || _boomSprite == null) return;

        StartCoroutine(BoomCoroutine());
    }

    private IEnumerator BoomCoroutine()
    {
        _isActive = true;

        Vector3 centerPosition = Vector3.zero;

        GameObject boomObject = Instantiate(_boomSprite, centerPosition, Quaternion.identity);

        GameObject particle = null;
        if (_particleEffectPrefab != null)
        {
            particle = Instantiate(_particleEffectPrefab, centerPosition, Quaternion.identity);
        }

        // 3초 대기
        yield return new WaitForSeconds(_particleDuration);

        // Boom과 파티클 제거
        Destroy(boomObject);
        if (particle != null)
        {
            Destroy(particle);
        }

        _isActive = false;
    }
}