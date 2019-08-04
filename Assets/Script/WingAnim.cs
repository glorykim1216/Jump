using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WingAnim : MonoBehaviour
{
    public Animator Anim;
    // Start is called before the first frame update
    void Start()
    {
       
    }


    public void StandMotion()
    {
        Anim.SetBool("jumpMotion", false);
        Anim.SetBool("standMotion", true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
