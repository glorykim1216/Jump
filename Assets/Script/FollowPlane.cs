using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlane : MonoBehaviour
{
    private Transform tr;
    public GameObject cube;
    Material planMat;
    float sum;
    Rigidbody player;
    // Start is called before the first frame update
    void Start()
    {
        tr = this.transform;
        //planMat = tr.GetComponent<Renderer>().material;
        //player = cube.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        tr.position = new Vector3(cube.transform.position.x - 35.60651f, -2.6f, cube.transform.position.z );
        // Debug.Log(player.velocity.magnitude);
        //sum -= player.velocity.magnitude/10000;
        //planMat.SetTextureOffset("_BumpMap", new Vector2(0, sum));
    }
}
