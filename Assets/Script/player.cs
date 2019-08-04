using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class player : MonoBehaviour
{
    public eMode gameMode = eMode.FirstWeak;
    public float upPower = 1;
    public float forwardPower = 2;
    public float jumpPower = 10;
    public float currJumpPower;
    public bool isJumping = false;
    public Transform tr;
    public Rigidbody rigid;
    public Transform cube;
    public bool firstJump = true;
    public float halfLife = 0.5f;
    public float speedIncreaseValue = 1;
    private ParticleSystem waterPs;
    ParticleSystem.MainModule psMain;
    int jumpCount;
    bool stayEnd;
    bool isFever;
    GameObject splashPrefab;

    private bool isAlive = true;

    // wall
    public Transform wall1;
    public Transform wall2;
    private float loopPosition;
    private float loopValue = 1000;
    private bool isMove = false;

    public Transform bestScoreWall;

    public float gage = 100;

    public float mass = 1;

    public Animator leftWing;
    public Animator rightWing;
    
    void Start()
    {
        splashPrefab = Resources.Load("Prefabs/Splash") as GameObject;

        currJumpPower = jumpPower;
        tr = this.GetComponent<Transform>();
        rigid = this.GetComponent<Rigidbody>();

        waterPs = GameObject.Find("waterPS").GetComponent<ParticleSystem>();
        psMain = waterPs.main;
        psMain.loop = false;

        // wall
        wall1 = GameObject.Find("wall1").transform;
        wall2 = GameObject.Find("wall2").transform;
        loopPosition = loopValue;

        bestScoreWall = GameObject.Find("BestScoreWall").transform;
        bestScoreWall.gameObject.SetActive(false);
        bestScoreWall.position = new Vector3(bestScoreWall.position.x, bestScoreWall.position.y, GameManager.Instance.BestScore);
    }
    public void Init()
    {
        forwardPower = GameManager.Instance.forwardPower;
        jumpPower = GameManager.Instance.jumpPower;
        halfLife = GameManager.Instance.halfLife;
        rigid.mass = mass;
    }

    void Update()
    {
        if (GameManager.Instance.isGamePlaying == false)
            return;

        

        // wall 이동
        if (tr.position.z > loopPosition)
            WallMove();

        if (GameManager.Instance.BestScore > 10 && tr.position.z >= GameManager.Instance.BestScore - 100 && bestScoreWall.gameObject.activeSelf == false)
            bestScoreWall.gameObject.SetActive(true);

        GameManager.Instance.distance = (int)tr.position.z;

        if (isAlive == false)
            return;

        if (firstJump == false && isFever == false && rigid.velocity.z < 0.2f)
        {
            GameManager.Instance.SetJudgement(eJudgement.Fail);
            StartCoroutine(GameManager.Instance.GameOver());
        }
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

    }

    private void FixedUpdate()
    {
        if (rigid.velocity.y < 0 && tr.position.y >= 2.0f)
        {
            isJumping = false;
        }
        else
            stayEnd = false;

        if (rigid.velocity.magnitude <= 1f)
        {
            
            stayEnd = true;
            psMain.loop = false;
            waterPs.Stop();
        }

        if (isFever == false)
            Jump();
    }
    void OnTriggerEnter(Collider collision) // 충돌한 대상의 collision을 얻는다.
    {
        GameObject Splash = MonoBehaviour.Instantiate(splashPrefab) as GameObject;
        // 실제 인스턴스 생성. GameObject name의 기본값은 Bullet (clone)
        Splash.name = "bullet"; // name을 변경
        Splash.transform.position = this.transform.position;
        
        leftWing.SetBool("jumpMotion", true);
        leftWing.SetBool("standMotion", false);
       
        rightWing.SetBool("jumpMotion", true);
        rightWing.SetBool("standMotion", false);
       
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

            gage -= 10;
            GameManager.Instance.SetGagebar(gage / 100);

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
                GameManager.Instance.SetJudgement(eJudgement.Excellent);
                // 진동
                if (GameManager.Instance.isVibration == true)
                    Vibration.Vibrate(GameManager.Instance.vibrationValue);
            }
            else if (tr.position.y >= -0.1f)
            {
                currJumpPower = jumpPower * 0.8f;
                GameManager.Instance.SetJudgement(eJudgement.Good);
                // 진동
                if (GameManager.Instance.isVibration == true)
                    Vibration.Vibrate((int)(GameManager.Instance.vibrationValue * 0.5f));
            }
            else
            {
                GameManager.Instance.SetJudgement(eJudgement.Fail);
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

            if (gage < 30)
                upPower *= halfLife;

            if (gage <= 0)
            {
                isFever = true;
                jumpCount = 0;
            }
        }
    }
    public void Fever()
    {
        isAlive = false;
        GameManager.Instance.isjudging = false;

        currJumpPower = jumpPower * 0.9f;
        // 진동
        if (GameManager.Instance.isVibration == true)
            Vibration.Vibrate((int)(GameManager.Instance.vibrationValue * 0.5f));

        rigid.velocity = new Vector3(0, 0, 1);
        rigid.AddForce((Vector3.up * upPower + Vector3.forward * forwardPower) * currJumpPower, ForceMode.Impulse);

        jumpCount++;
        if (jumpCount > 15)
        {
            isFever = false;
            StartCoroutine("GameOver");
        }

        leftWing.SetBool("jumpMotion", false);
        leftWing.SetBool("standMotion", true);

        rightWing.SetBool("jumpMotion", false);
        rightWing.SetBool("standMotion", true);
    }
    IEnumerator GameOver()
    {
        yield return new WaitForEndOfFrame();
        while (true)
        {
            if (rigid.velocity.z < 0.2f)
                break;
            yield return null;
        }
        StartCoroutine(GameManager.Instance.GameOver());
    }
    // wall loop
    void WallMove()
    {
        loopPosition += loopValue;

        if (isMove == false)
            wall1.position = new Vector3(wall1.position.x, wall1.position.y, loopPosition);
        else
            wall2.position = new Vector3(wall2.position.x, wall2.position.y, loopPosition);

        isMove = !isMove;
    }
}
