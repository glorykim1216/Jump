using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    public eMode gameMode = eMode.FirstWeak;
    public float speed = 5;
    public float jumpPower = 15;
    private float currJumpPower;
    public bool isJumping = false;
    private bool isAlive = true;
    public Transform tr;
    public Rigidbody rigid;
    public Transform cube;
    public bool firstJump = true;
    private ParticleSystem waterPs;
    ParticleSystem.MainModule psMain;
    bool stayEnd;
    void Start()
    {
        currJumpPower = jumpPower;
        tr = this.GetComponent<Transform>();
        rigid = this.GetComponent<Rigidbody>();

        waterPs = GameObject.Find("CFX2_Blood").GetComponent<ParticleSystem>();
        psMain = waterPs.main;
        psMain.loop = false;
    }

    void Update()
    {
        {
            if (Input.GetMouseButtonDown(0))
                isJumping = true;
        }

        if (this.GetComponent<Rigidbody>().velocity.magnitude <= 3f)
        {
            psMain.loop = false;
            waterPs.Stop();
        }
        //Debug.Log(this.GetComponent<Rigidbody>().velocity.magnitude);
    }

    private void FixedUpdate()
    {
        if (isAlive == true)
            Jump();
    }
    void OnTriggerEnter(Collider collision) // 충돌한 대상의 collision을 얻는다.
    {
        GameObject prefab = Resources.Load("Prefabs/Splash") as GameObject;

        GameObject Splash = MonoBehaviour.Instantiate(prefab) as GameObject;
        // 실제 인스턴스 생성. GameObject name의 기본값은 Bullet (clone)
        Splash.name = "bullet"; // name을 변경
        Splash.transform.position = this.transform.position;

        // 진동
        Vibration.Vibrate();
        // Debug.Log(collision.gameObject.name + "과 부딪혔습니다.");
    }
    void OnTriggerStay(Collider collision) // 충돌한 대상의 collision을 얻는다.
    {
        if (!stayEnd)
        {
            psMain.loop = true;
            waterPs.Play();
        }

        // Debug.Log(collision.gameObject.name + "과 부딪혔습니다.");
    }
    void OnTriggerExit(Collider collision) // 충돌한 대상의 collision을 얻는다.
    {
        psMain.loop = false;
        stayEnd = true;
        // Debug.Log(collision.gameObject.name + "과 부딪혔습니다.");
    }
    void Jump()
    {
        if (tr.position.y >= 0.7f)
        {
            stayEnd = false;
            return;
        }

        if (isJumping == true)
        {
            if (gameMode == eMode.FirstWeak)
            {
                if (tr.position.y >= 0.4f)
                    currJumpPower *= 1.2f;
                else if (tr.position.y >= -0.1f)
                    currJumpPower = jumpPower;
                else
                {
                    isAlive = false;
                    return;
                }

                rigid.AddForce((Vector3.up * 2 + Vector3.forward) * currJumpPower);
                isJumping = false;
            }
            else if (gameMode == eMode.FirstPower)
            {
                if (firstJump == true)
                {
                    currJumpPower = jumpPower = 90;
                    rigid.AddForce((Vector3.up + Vector3.forward) * currJumpPower);
                    firstJump = false;
                    isJumping = false;

                    return;
                }

                if (tr.position.y >= 0.4f)
                    currJumpPower = jumpPower * 0.9f;
                else if (tr.position.y >= -0.1f)
                    currJumpPower = jumpPower * 0.8f;
                else
                {
                    isAlive = false;
                    return;
                }

                rigid.AddForce((Vector3.up * 2 + Vector3.forward) * currJumpPower);
                isJumping = false;
            }

        }
    }


}
