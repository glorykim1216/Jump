using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    public Transform target;       
    public float offsetX = 0.0f;  
    public float offsetY = 25.0f; 
    public float offsetZ = -35.0f;
    public float speed = 5;
    private Transform tr;             // 카메라 
    Vector3 camPos;

    void Start()
    {
        tr = this.transform;        
    }
    
    void FixedUpdate()
    {
        camPos.x = target.position.x + offsetX;
        camPos.y = target.position.y + offsetY;
        camPos.z = target.position.z + offsetZ;

        tr.position = Vector3.Lerp(transform.position, camPos, speed * Time.deltaTime);

        //tr.LookAt(target);
    }
}
