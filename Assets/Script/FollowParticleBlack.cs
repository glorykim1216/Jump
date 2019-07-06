using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowParticleBlack : MonoBehaviour
{
    private Transform tr;
    public GameObject cube;

    void Start()
    {
        tr = this.transform;
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        tr.position = new Vector3(cube.transform.position.x, 0, cube.transform.position.z);
    }
}
