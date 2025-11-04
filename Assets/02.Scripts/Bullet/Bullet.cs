using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float _speed = 5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.up * Time.deltaTime * _speed);
    }
}
