using Unity.VisualScripting;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public Transform player;
    private Vector3 offset= new Vector3(0, 0, -10);
    

    void Update()
    {
        if(!player.IsUnityNull())
            transform.position = player.position + offset;

    }
}
