using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float Speed = 5f;
    // boundary
    private float xMin = -2f;
    private float xMax =  2f;
    private float yMin = -3f;
    private float yMax =  3f;
    // speed up, speed down
    private float speedStep = 1f;
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
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Speed += speedStep;
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            Speed -= speedStep;
            if(Speed <=0) Speed = 0;
        }
        newPosition = position + direction * (Speed * Time.deltaTime);
        if (newPosition.x > xMax)
        {
            newPosition.x = xMin;
        }
        else if (newPosition.x < xMin)
        {
            newPosition.x = xMax;
        }
        
        if (newPosition.y > yMax)
        {
            newPosition.y = yMin;
        }
        else if (newPosition.y < yMin)
        {
            newPosition.y = yMax;
        }
        transform.position = newPosition;
    }
}
