using UnityEngine;

public class Obstacle  : MonoBehaviour
{

    public float minSize = 0.5f;
    public float maxSize = 2.0f;
    public float minSpeed = 50f;
    public float maxSpeed = 150f;
    public float maxSpinSpeed = 10f;
    public GameObject obstaclesCollisionPrefab;
    
    Rigidbody2D rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        float randomSize = Random.Range(minSize, maxSize);
        transform.localScale = new Vector3(randomSize, randomSize, 1);

        rb = GetComponent<Rigidbody2D>();

        speedUp(rb);

        float randomTorque = Random.Range(-maxSpinSpeed, maxSpinSpeed);
        rb.AddTorque(randomTorque);
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 contactPoint = collision.GetContact(0).point;
        GameObject obstaclesCollision =  Instantiate(obstaclesCollisionPrefab, contactPoint, Quaternion.identity);

        Destroy(obstaclesCollision, 3f);
        
    }
    
    public void speedUp(Rigidbody2D obj)
    {
        float randomSpeed = Random.Range(minSpeed, maxSpeed) ;// transform.localScale.x;
        Vector2 randomDirection = Random.insideUnitCircle;
        obj.AddForce(randomDirection * randomSpeed);   
    }
    
   

    
}
