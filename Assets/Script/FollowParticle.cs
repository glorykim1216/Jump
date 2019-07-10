using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowParticle : MonoBehaviour
{
    private Transform tr;
    public GameObject cube;
    private float tempY;                        
    void Start()
    {
        tr = this.transform;
    }


    // Update is called once per frame
    void LateUpdate()
    {
        tempY = cube.transform.position.y - tr.position.y;
        if (tempY < 0.6f)
            tempY = 0.6f;
        if(tempY<2.5f)
            tr.localScale = new Vector3(1* tempY, 1* tempY, 1* tempY);
        tr.position = new Vector3( cube.transform.position.x,0.1f, cube.transform.position.z);
    }
}
