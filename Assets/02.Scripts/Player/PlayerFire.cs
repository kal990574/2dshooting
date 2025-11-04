using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    // 목표: 스페이스바를 누르면 총알을 만들어서 발사하고 싶다.

    // 필요 속성
    [Header("총알 프리팹")]
    public GameObject BulletPrefab1;
    public GameObject BulletPrefab2;
    [Header("총구")]
    public Transform FirePosition1;
    public Transform FirePosition2;
    public Transform FirePosition3;
    public Transform FirePosition4;
    [Header("발사 설정")]
    public float FireCooldown = 0.6f;
    
    private float _lastFireTime = -1f;

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            // 쿨타임
            if (Time.time >= _lastFireTime + FireCooldown)
            {
                Fire();
                _lastFireTime = Time.time;
            }
        }
    }

    private void Fire()
    {
        GameObject bullet1 = Instantiate(BulletPrefab1);
        bullet1.transform.position = FirePosition1.position;
        GameObject bullet2 = Instantiate(BulletPrefab1);
        bullet2.transform.position = FirePosition2.position;
        GameObject bullet3 = Instantiate(BulletPrefab2);
        bullet3.transform.position = FirePosition3.position;
        GameObject bullet4 = Instantiate(BulletPrefab2);
        bullet4.transform.position = FirePosition4.position;
    }
}
