
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class NewObstacle : MonoBehaviour
{

    
    public float minRadius = 14f;
    public float maxRadius = 18f;
    public int poolSize = 20;
    public GameObject obstacle;
    public Transform parent;
    public Transform player;
    private GameObject obstacleCopy;
    private List<GameObject> pool;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pool = new List<GameObject>();

        for(int i =0; i < poolSize; i++)
        {
            pool.Add(cloneObstacle());
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach(var obj in pool)
        {
            ifToDespawnObstacle(obj);
            Vector3 dir = (player.position - obj.transform.position).normalized;
            obj.GetComponent<Rigidbody2D>().AddForce(dir);
        }
       
    }

    GameObject cloneObstacle()
    {
         
        obstacleCopy = Instantiate(obstacle, generateRandomPosition(), Quaternion.identity, parent);
        
        SpriteRenderer sprite = obstacleCopy.GetComponent<SpriteRenderer>();
        sprite.color = Color.purple;


        obstacleCopy.SetActive(false);
        return obstacleCopy;
    }

    public void spawnObstacle()
    {
       
        foreach(var obj in pool)
        {
            if (!obj.activeInHierarchy)
            {               
                obj.transform.position = generateRandomPosition();

                
                obj.SetActive(true);
                return ;
            }
        }

        GameObject newobj = cloneObstacle();
        
        newobj.SetActive(true);
        pool.Add(newobj);
        return ;


    }

    void ifToDespawnObstacle(GameObject obj)
    {
        if((player.position - obj.transform.position).magnitude > 30)
        {
            obj.SetActive(false);
        }
    }


    Vector3 generateRandomPosition()
    {
        float angle = Random.Range(0, Mathf.PI*2);
        float radius = Random.Range(minRadius, maxRadius);

        Vector3 pos = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) *radius;
        return player.position + pos;
    }

    

}
