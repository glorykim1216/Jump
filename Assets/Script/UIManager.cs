using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Lofle.Tween;
public class UIManager : MonoSingleton<UIManager>
{
    public GameObject OptionPopup;
    public GameObject LobbyUI;
    public GameObject InGameUI;
    public GameObject ResultUI;
   
    public Transform ArrowRot;

    public Text goldText;
    public Text bestScoreText;

    public Image gagebar;
    public Text currScoreText;

    public Text judgement;

    public Button vibrationBtn;
    public Button gameOverGoldBtn;

    public GameObject newBestScore;
    public GameObject resultNewBestScore;

    public GameObject SkinScrollViewObj;

    public Sprite vibOn;
    public Sprite vibOff;

    public override void Init(){}

    void Start()
    {
        //tween 시작시 2번클릭 방지
        //tween.PlayReverse();

        vibrationBtn.onClick.AddListener(VibrationOption);
        gameOverGoldBtn.onClick.AddListener(ViewAD);

        VibrationOnOffCheck();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
          
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
           
        }
    }

    public void SkinButton()
    {
        //트윈 위치 설정 (0,0,0,)으로 위치 이동하는 현상 제거
        //tween.From = new Vector3(tween.transform.localPosition.x, 300, 0);
        //tween.To = new Vector3(tween.transform.localPosition.x, 0, 0);
        //if (tween.isForward)
        //{
        //    ArrowRot.localRotation = Quaternion.Euler(new Vector3(0, 0, -50));
        //    tween.PlayReverse();
        //}

        //else
        //{
        //    ArrowRot.localRotation = Quaternion.Euler(new Vector3(0, 0, 130));
        //    tween.PlayForward();
        //}
        if(!SkinScrollViewObj.activeSelf)
            SkinScrollViewObj.SetActive(true);
        else
            SkinScrollViewObj.SetActive(false);
    }

    public void SkinSelectedValue()
    {
        Debug.Log("작동 잘됨");
    }
    // 옵션창 On
    public void OpenOption()
    {
        OptionPopup.SetActive(true);
    }
    // 옵션창 Off
    public void CloseOption()
    {
        OptionPopup.SetActive(false);
    }
    // 게임시작
    public void StartGame()
    {
        LobbyUI.SetActive(false);
        InGameUI.SetActive(true);
        GameManager.Instance.isGamePlaying = true;
    }
    // 진동 옵션
    public void VibrationOption()
    {
        GameManager.Instance.isVibration = !GameManager.Instance.isVibration;
        VibrationOnOffCheck();
    }
    // 진동 On/Off 체크
    public void VibrationOnOffCheck()
    {
        if(GameManager.Instance.isVibration==true)
        {
            vibrationBtn.GetComponent<Image>().sprite = vibOn;
        }
        else
        { 
            vibrationBtn.GetComponent<Image>().sprite = vibOff;
        }
    }

    public void ViewAD()
    {
        GameManager.Instance.ReStart();
    }
}
