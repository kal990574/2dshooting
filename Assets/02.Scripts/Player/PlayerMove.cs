using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [Header("Boundary")]
    [SerializeField] private float _xMin = -2f;
    [SerializeField] private float _xMax =  2f;
    [SerializeField] private float _yMin = -3f;
    [SerializeField] private float _yMax =  3f;
    
    [Header("Speed")]
    [SerializeField] private float _speedStep = 1f;
    [SerializeField] private float _speed = 5f;
    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Debug.Log($"h:{h}, v:{v}");
        
        Vector2 direction = new Vector2(h, v).normalized;
        
        Vector2 position = transform.position;
        Vector2 newPosition;
        
        if (Input.GetKeyDown(KeyCode.Q))
        {
            _speed += _speedStep;
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            _speed -= _speedStep;
            if(_speed <=0) _speed = 0;
        }
        
        newPosition = position + direction * (_speed * Time.deltaTime);
        
        if (newPosition.x > _xMax)
        {
            newPosition.x = _xMin;
        }
        else if (newPosition.x < _xMin)
        {
            newPosition.x = _xMax;
        }
        
        if (newPosition.y > _yMax)
        {
            newPosition.y = _yMin;
        }
        else if (newPosition.y < _yMin)
        {
            newPosition.y = _yMax;
        }
        transform.position = newPosition;
    }
}
