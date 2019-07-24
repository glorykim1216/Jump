using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Lofle.Tween;
using UnityEngine.EventSystems;
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
    public GameObject EffectScrollViewObj;
    public Sprite vibOn;
    public Sprite vibOff;

    public List<int> skinBuyList;
    CheckSkin[] CheckSkinData;

    public override void Init(){}

    private void Awake()
    {
        
    }

    void Start()
    {
        //tween 시작시 2번클릭 방지
        //tween.PlayReverse();
        SkinScrollViewObj.SetActive(true);
        vibrationBtn.onClick.AddListener(VibrationOption);
        gameOverGoldBtn.onClick.AddListener(ViewAD);

        VibrationOnOffCheck();


        
        GameManager.Instance.OpenSkinList = 231;
        string test;
        test = string.Format("{0:d20}", int.Parse(System.Convert.ToString(GameManager.Instance.OpenSkinList, 2)));
        Debug.Log(test);

        CheckSkinData = this.gameObject.GetComponentsInChildren<CheckSkin>();

        foreach (CheckSkin k in CheckSkinData)
        {
            
            k.CheckImg.SetActive(false);

        }

        for (int i = 0; i < CheckSkinData.Length; i++)
        {
            CheckSkinData[i].SkinNumber = i + 1;
            if (test[i].ToString() == "1")
            {
                Debug.Log("!!!!");
                CheckSkinData[i].moneyText.text = "OK        ";
                CheckSkinData[i].moneyImg.enabled = false;
                CheckSkinData[i].BuyCheck = true;
              
            }
            else
            {
                CheckSkinData[i].BuyCheck = false;
            }
               
            if(CheckSkinData[i].SkinNumber == GameManager.Instance.CurrSkin)
                CheckSkinData[i].CheckImg.SetActive(true);
        }


        SkinScrollViewObj.SetActive(false);

    }
    string Num;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            CheckSkinData = this.gameObject.GetComponentsInChildren<CheckSkin>();
            Num = null;
            foreach (CheckSkin k in CheckSkinData)
            {
                if (k.BuyCheck)
                    Num += "1";
                else
                    Num += "0";
            }

            //Debug.Log(System.Convert.ToInt32(Num,2));

            Debug.Log(Num);
        }
        //if (Input.GetKeyDown(KeyCode.B))
        //{
        //    int testNum = 2;

        //    string test;
        //    test = string.Format("{0:d20}", int.Parse(System.Convert.ToString(testNum, 2)));
        //    Debug.Log(test);

        //    CheckSkinData = this.gameObject.GetComponentsInChildren<CheckSkin>();


        //    for(int i = 0; i< CheckSkinData.Length; i++)
        //    {
        //        if(test[i].ToString() == "1")
        //        {
        //            Debug.Log("!!!!");
        //            CheckSkinData[i].BuyCheck = true;
        //        }
        //        else
        //            CheckSkinData[i].BuyCheck = false;
        //    }
          
        //}
    }

    public void CheckSkin(int CurrentSkin)
    {
        if(EventSystem.current.currentSelectedGameObject.GetComponent<CheckSkin>().BuyCheck)
        {
            foreach (CheckSkin k in CheckSkinData)
            {
                k.CheckImg.SetActive(false);
            }
            GameManager.Instance.CurrSkin = CurrentSkin;

            EventSystem.current.currentSelectedGameObject.GetComponent<CheckSkin>().CheckImg.SetActive(true);
        }
        Debug.Log("CurrentSkin : " + GameManager.Instance.CurrSkin);

        //Debug.Log(EventSystem.current.currentSelectedGameObject.name);
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

    public void EffectButton()
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
        if (!EffectScrollViewObj.activeSelf)
            EffectScrollViewObj.SetActive(true);
        else
            EffectScrollViewObj.SetActive(false);
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
