using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    // 필요 속성
    [Header("총알 프리팹")]
    public GameObject MainBulletPrefab;
    public GameObject SubBulletPrefab;
    
    [Header("총구")]
    public Transform MainFirePositionLeft;
    public Transform MainFirePositionRight;
    public Transform SubFirePositionLeft;
    public Transform SubFirePositionRight;
    
    [Header("발사 설정")]
    public float FireCooldown = 0.6f;
    
    private float _lastFireTime = -1f;
    private int _fireMode = 1; 

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            _fireMode = 1;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            _fireMode = 2;
        }

        // 자동 공격
        if (_fireMode == 1)
        {
            if (Time.time >= _lastFireTime + FireCooldown)
            {
                Fire();
                _lastFireTime = Time.time;
            }
        }
        // 수동 공격
        else if (_fireMode == 2 && Input.GetKey(KeyCode.Space))
        {
            if (Time.time >= _lastFireTime + FireCooldown)
            {
                Fire();
                _lastFireTime = Time.time;
            }
        }
    }

    private void Fire()
    {
        GameObject mainBulletLeft = Instantiate(MainBulletPrefab);
        mainBulletLeft.transform.position = MainFirePositionLeft.position;
        GameObject mainBulletRight = Instantiate(MainBulletPrefab);
        mainBulletRight.transform.position = MainFirePositionRight.position;
        GameObject subBulletLeft = Instantiate(SubBulletPrefab);
        subBulletLeft.transform.position = SubFirePositionLeft.position;
        GameObject subBulletRight = Instantiate(SubBulletPrefab);
        subBulletRight.transform.position = SubFirePositionRight.position;
    }
}
