using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class player : MonoBehaviour
{
    public eMode gameMode = eMode.FirstWeak;
    public float speed = 5;
    public float upPower = 1;
    public float forwardPower = 1;
    public float jumpPower = 15;
    public float currJumpPower;
    public bool isJumping = false;
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
    bool isFever;
    GameObject splashPrefab;
    void Start()
    {
        splashPrefab = Resources.Load("Prefabs/Splash") as GameObject;

        currJumpPower = jumpPower;
        tr = this.GetComponent<Transform>();
        rigid = this.GetComponent<Rigidbody>();

        waterPs = GameObject.Find("waterPS").GetComponent<ParticleSystem>();
        psMain = waterPs.main;
        psMain.loop = false;
    }

    void Update()
    {
        if (GameManager.Instance.isGamePlaying == false)
            return;
        //#if UNITY_ANDROID && !UNITY_EDITOR
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began)
                {
                    if (EventSystem.current.IsPointerOverGameObject(0) == false)    // UI 터치 구분
                    {
                        isJumping = true;
                    }
                }
                //for (int i = 0; i < Input.touchCount; i++)
                //{
                //    if (EventSystem.current.IsPointerOverGameObject(i) == false)    // UI 터치 구분
                //    {
                //        isJumping = true;
                //        break;
                //    }
                //}
            }
        }
        //#else
        if (Input.GetMouseButtonDown(0))
            if (EventSystem.current.IsPointerOverGameObject() == false) // UI 클릭 구분
                isJumping = true;
        //#endif
        if (rigid.velocity.magnitude <= 1f)
        {
            psMain.loop = false;
            waterPs.Stop();
        }

        if (firstJump == false && isFever == false && rigid.velocity.z < 0.5f)
        {
            GameManager.Instance.SetJudement("Fail");
            StartCoroutine(GameManager.Instance.GameOver());
        }

    }

    private void FixedUpdate()
    {
        if (rigid.velocity.y < 0 && tr.position.y >= 2.0f)
        {
            isJumping = false;
        }
        else
            stayEnd = false;

        Jump();
    }
    void OnTriggerEnter(Collider collision) // 충돌한 대상의 collision을 얻는다.
    {
        GameObject Splash = MonoBehaviour.Instantiate(splashPrefab) as GameObject;
        // 실제 인스턴스 생성. GameObject name의 기본값은 Bullet (clone)
        Splash.name = "bullet"; // name을 변경
        Splash.transform.position = this.transform.position;
    }
    void OnTriggerStay(Collider collision) // 충돌한 대상의 collision을 얻는다.
    {
        if (collision.name == "Water" && isFever == true)
        {
            Fever();
        }
        if (!stayEnd)
        {
            psMain.loop = true;
            waterPs.Play();
        }
    }
    void OnTriggerExit(Collider collision) // 충돌한 대상의 collision을 얻는다.
    {
        psMain.loop = false;
        stayEnd = true;
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
            isJumping = false;

            GameManager.Instance.gage -= 10;
            GameManager.Instance.SetGagebar(GameManager.Instance.gage / 100);

            if (firstJump == true)
            {
                currJumpPower = jumpPower;
                rigid.AddForce((Vector3.up * upPower + Vector3.forward * forwardPower) * currJumpPower, ForceMode.Impulse);
                firstJump = false;
                return;
            }

            if (tr.position.y >= 0.4f)
            {
                currJumpPower = jumpPower * 0.9f;
                GameManager.Instance.SetJudement("Excellent !!");
                // 진동
                if (GameManager.Instance.isVibration == true)
                    Vibration.Vibrate(GameManager.Instance.vibrationValue);
            }
            else if (tr.position.y >= -0.1f)
            {
                currJumpPower = jumpPower * 0.8f;
                GameManager.Instance.SetJudement("Good");
                // 진동
                if (GameManager.Instance.isVibration == true)
                    Vibration.Vibrate((int)(GameManager.Instance.vibrationValue * 0.5f));
            }
            else
            {
                GameManager.Instance.SetJudement("Fail");
                StartCoroutine(GameManager.Instance.GameOver());
                return;
            }

            if (jumpCount < 2)
            {
                jumpCount++;
            }

            rigid.velocity = new Vector3(0, 0, 0);
            rigid.AddForce((Vector3.up * upPower + Vector3.forward * forwardPower) * currJumpPower, ForceMode.Impulse);

            if (forwardPower < 15f)
                forwardPower += speedIncreaseValue;

            if (GameManager.Instance.gage < 30)
                upPower *= upValue;

            if (GameManager.Instance.gage <= 0)
            {
                isFever = true;
                jumpCount = 0;
            }
        }
    }
    public void Fever()
    {
        currJumpPower = jumpPower * 0.9f;
        //GameManager.Instance.SetJudement("Excellent !!");
        // 진동
        if (GameManager.Instance.isVibration == true)
            Vibration.Vibrate((int)(GameManager.Instance.vibrationValue * 0.5f));

        rigid.velocity = new Vector3(0, 0, 0);
        rigid.AddForce((Vector3.up * upPower + Vector3.forward * forwardPower) * currJumpPower, ForceMode.Impulse);

        jumpCount++;
        if (jumpCount > 15)
        {
            isFever = false;
            StartCoroutine(GameManager.Instance.GameOver());
        }
    }


}
