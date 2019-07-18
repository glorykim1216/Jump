using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class player : MonoBehaviour
{
    public GameManager gameManger;
    public eMode gameMode = eMode.FirstWeak;
    public float speed = 5;
    public float upPower = 1;
    public float forwardPower = 1;
    public float jumpPower = 15;
    public float currJumpPower;
    public bool isJumping = false;
    private bool isAlive = true;
    public Transform tr;
    public Rigidbody rigid;
    public Transform cube;
    public bool firstJump = true;
    public float upValue = 0.5f;
    public float speedIncreaseValue = 1;
    private ParticleSystem waterPs;
    ParticleSystem.MainModule psMain;
    int jumpCount;
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
#if UNITY_ANDROID && !UNITY_EDITOR
        if (Input.touchCount > 0)
        {
            //for (int i = 0; i < Input.touchCount; i++)
            {
                if (EventSystem.current.IsPointerOverGameObject(0))
                    return;
                else
                    isJumping = true;
            }
        }
#else
        if (Input.GetMouseButtonDown(0))
            if (EventSystem.current.IsPointerOverGameObject() == false)
                isJumping = true;
#endif

        if (gameManger.gage <= 0)
        {
            isAlive = false;
        }

        if (rigid.velocity.magnitude <= 1f)
        {
            psMain.loop = false;
            waterPs.Stop();
        }
        //Debug.Log(rigid.velocity.magnitude);
    }

    private void FixedUpdate()
{
    if (rigid.velocity.y < 0 && tr.position.y >= 2.0f)
    {
        isJumping = false;
    }
    else
            stayEnd = false;

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
    Vibration.Vibrate(500);
    //Handheld.Vibrate();

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
        //stayEnd = false;
        return;
    }

    if (isJumping == true)
    {
        gameManger.gage -= 10;
        gameManger.SetGagebar(gameManger.gage / 100);

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
            rigid.velocity = new Vector3(0, 0, 0);
            rigid.AddForce((Vector3.up * 2 + Vector3.forward) * currJumpPower, ForceMode.Impulse);
            isJumping = false;
        }
        else if (gameMode == eMode.FirstPower)
        {

            if (firstJump == true)
            {
                currJumpPower = jumpPower;
                rigid.AddForce((Vector3.up * upPower + Vector3.forward * forwardPower) * currJumpPower, ForceMode.Impulse);
                firstJump = false;
                isJumping = false;

                return;
            }

            if (tr.position.y >= 0.4f)
            {
                currJumpPower = jumpPower * 0.9f;
                gameManger.SetJudement("Excellent !!");
            }
            else if (tr.position.y >= -0.1f)
            {
                currJumpPower = jumpPower * 0.8f;
                gameManger.SetJudement("Good");
            }
            else
            {
                gameManger.SetJudement("Fail");
                isAlive = false;
                return;
            }

            if (jumpCount < 2)
            {
                jumpCount++;
                currJumpPower = jumpPower;
                rigid.velocity = new Vector3(0, 0, 0);
                rigid.AddForce((Vector3.up * upPower + Vector3.forward * forwardPower) * currJumpPower, ForceMode.Impulse);
                return;
            }

            rigid.velocity = new Vector3(0, 0, 0);
            rigid.AddForce((Vector3.up * upPower + Vector3.forward * forwardPower) * currJumpPower, ForceMode.Impulse);

            if (forwardPower < 15f)
                forwardPower += speedIncreaseValue;

            if (gameManger.gage < 30)
                upPower *= upValue;
            isJumping = false;
        }

    }
}


}
