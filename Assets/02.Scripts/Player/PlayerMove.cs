using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Debug.Log($"h:{h}, v:{v}");
        
        Vector2 direction = new Vector2(h, v);
        Debug.Log($"direction:{direction}");
        
        Vector2 position = transform.position;

        Vector2 newPosition;
        newPosition = position + direction;
        transform.position = newPosition;

    }
}
