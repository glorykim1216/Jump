using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Lofle.Tween;
public class UIManager : MonoBehaviour
{
    public GameObject OptionPopup;
    public GameObject LobbyUI;
    public GameObject InGameUI;
    public TweenPosition tween;
    public Transform ArrowRot;

    public Text goldText;
    public Text bestScoreText;

    public Image gagebar;
    public Text currScoreText;

    public Text judgement;

    public Button vibrationBtn;

    public GameObject newBestScore;

    // Start is called before the first frame update
    void Start()
    {
        //tween 시작시 2번클릭 방지
        tween.PlayReverse();

        GameManager.Instance.UI_Manager = this;
        vibrationBtn.onClick.AddListener(VibrationOption);
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
        tween.From = new Vector3(tween.transform.localPosition.x, 300, 0);
        tween.To = new Vector3(tween.transform.localPosition.x, 0, 0);
        if (tween.isForward)
        {
            ArrowRot.localRotation = Quaternion.Euler(new Vector3(0, 0, -50));
            tween.PlayReverse();
        }
         
        else
        {
            ArrowRot.localRotation = Quaternion.Euler(new Vector3(0, 0, 130));
            tween.PlayForward();
        }
           
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
            vibrationBtn.GetComponentInChildren<Text>().text = "On";
        }
        else
        { 
            vibrationBtn.GetComponentInChildren<Text>().text = "Off";
        }
    }
}
