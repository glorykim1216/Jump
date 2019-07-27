using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Lofle.Tween;
using UnityEngine.EventSystems;
public class UIManager : MonoSingleton<UIManager>
{
    private int rewardGold = 50;

    public GameObject OptionPopup;
    public GameObject LobbyUI;
    public GameObject InGameUI;
    public GameObject ResultUI;

    public Transform ArrowRot;

    public Text goldText;
    public Text bestScoreText;

    public Image gagebar;
    public Text currScoreText;

    public Button vibrationBtn;
    public Button resultGoldBtn;
    public Button resultGoldDoubleBtn;

    public Text resultScoreText;
    public Text resultGoldText;
    public Text resultGoldBtnText;

    public GameObject SkinScrollViewObj;
    public GameObject EffectScrollViewObj;
    public Sprite vibOn;
    public Sprite vibOff;

    public GameObject judgementFailImage;
    public GameObject judgementGoodImage;
    public GameObject judgementExceImage;
    public GameObject newBestScoreImage;
    public GameObject resultNewBestScoreImage;

    public List<int> skinBuyList;
    CheckSkin[] CheckSkinData;

    public GameObject BuyIMG;
    public int priceTemp;
    public Texture renderTemp;
    public GameObject ObjTemp;
    public int SkinNumTemp;

    public RectTransform crossBanner;
    public GameObject skinEffectObj;

    public override void Init() { }

    void Start()
    {
        //tween 시작시 2번클릭 방지
        //tween.PlayReverse();
        skinEffectObj.SetActive(false);
        SkinScrollViewObj.SetActive(true);
        vibrationBtn.onClick.AddListener(VibrationOption);
        resultGoldBtn.onClick.AddListener(ViewAD);
        crossBanner.GetComponent<Button>().onClick.AddListener(OpenPlayStore);
        VibrationOnOffCheck();
        //GameManager.Instance.skinADState = false;


        //GameManager.Instance.OpenSkinList = 231;
        Debug.Log("GameManager.Instance.OpenSkinList" + GameManager.Instance.OpenSkinList);
        string test;
        test = System.Convert.ToString(GameManager.Instance.OpenSkinList, 2).PadLeft(20, '0');
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

            if (CheckSkinData[i].SkinNumber == GameManager.Instance.CurrSkin)
                CheckSkinData[i].CheckImg.SetActive(true);
        }

        BuyIMG.SetActive(false);
        SkinScrollViewObj.SetActive(false);
        GameObject.Find("Player").GetComponentInChildren<SkinnedMeshRenderer>().material = Resources.Load("Material/Icem" + GameManager.Instance.CurrSkin.ToString()) as Material;

        GameManager.Instance.Init();

        StartCoroutine("cor_CrossBannerAnim");
    }
    string Num;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log(GameManager.Instance.CurrSkin);
        }
    }

    public void FreeSkin()
    {
        //GameManager.Instance.skinADState = true;
        ADManager.Instance.ShowInterstitialAd();
        

        BuyIMG.SetActive(false);
        SkinScrollViewObj.SetActive(false);
    }

    public void CheckSkin(int CurrentSkin)
    {
        if (EventSystem.current.currentSelectedGameObject.GetComponent<CheckSkin>().BuyCheck)
        {
            foreach (CheckSkin k in CheckSkinData)
            {
                k.CheckImg.SetActive(false);
            }
            GameManager.Instance.CurrSkin = CurrentSkin;

            EventSystem.current.currentSelectedGameObject.GetComponent<CheckSkin>().CheckImg.SetActive(true);

            //구입후 이미지 변환 작성해야됨;
            GameObject.Find("Player").GetComponentInChildren<SkinnedMeshRenderer>().material = Resources.Load("Material/Icem" + GameManager.Instance.CurrSkin.ToString()) as Material;
            
        }
        else
        {
            priceTemp = EventSystem.current.currentSelectedGameObject.GetComponent<CheckSkin>().needMoney;
            renderTemp = EventSystem.current.currentSelectedGameObject.transform.parent.GetComponentInChildren<RawImage>().texture;
            ObjTemp = EventSystem.current.currentSelectedGameObject;
            GameManager.Instance.PlayerMat = GameObject.Find("Player").GetComponentInChildren<SkinnedMeshRenderer>().material;
            SkinNumTemp = CurrentSkin;
            GameManager.Instance.TempMat = Resources.Load("Material/Icem" + SkinNumTemp.ToString()) as Material;

            
            BuyIMG.SetActive(true);
            //BuyIMG.transform.Find("price").GetComponent<Text>().text = EventSystem.current.currentSelectedGameObject.GetComponent<CheckSkin>().needMoney.ToString();


        }
        Debug.Log("CurrentSkin : " + GameManager.Instance.CurrSkin);
        Debug.Log("priceTemp : " + priceTemp);
        //Debug.Log(EventSystem.current.currentSelectedGameObject.name);
    }

    public void Purchase()
    {
        if (GameManager.Instance.Gold > ObjTemp.GetComponent<CheckSkin>().needMoney)
        {
            GameManager.Instance.Gold -= ObjTemp.GetComponent<CheckSkin>().needMoney;

            ObjTemp.GetComponent<CheckSkin>().BuyCheck = true;

            Num = null;
            foreach (CheckSkin k in CheckSkinData)
            {
                if (k.BuyCheck)
                    Num += "1";
                else
                    Num += "0";
            }
            /////
            //저장
            GameManager.Instance.OpenSkinList = System.Convert.ToInt32(Num, 2);
            string test;
            test = System.Convert.ToString(GameManager.Instance.OpenSkinList, 2).PadLeft(20, '0');
            Debug.Log(test);

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

                if (CheckSkinData[i].SkinNumber == GameManager.Instance.CurrSkin)
                    CheckSkinData[i].CheckImg.SetActive(true);
            }

            BuyIMG.SetActive(false);
            ////


            foreach (CheckSkin k in CheckSkinData)
            {
                k.CheckImg.SetActive(false);
            }
            GameManager.Instance.CurrSkin = SkinNumTemp;

            ObjTemp.GetComponent<CheckSkin>().CheckImg.SetActive(true);


            //구입후 이미지 변환 작성해야됨;
            GameObject.Find("Player").GetComponentInChildren<SkinnedMeshRenderer>().material = Resources.Load("Material/Icem" + GameManager.Instance.CurrSkin.ToString()) as Material;
        }
    }

    public void SkinButton()
    {
        if (!SkinScrollViewObj.activeSelf)
        {
            skinEffectObj.SetActive(true);
            SkinScrollViewObj.SetActive(true);
            skinEffectObj.transform.Find("Skin").gameObject.SetActive(true);
            skinEffectObj.transform.Find("Effect").gameObject.SetActive(false);
        }
            
        else
        {
            BuyIMG.SetActive(false);
            SkinScrollViewObj.SetActive(false);
            skinEffectObj.SetActive(false);
        }

    }
    public void BackButton()
    {
        BuyIMG.SetActive(false);
    }
    public void EffectButton()
    {
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
        StopCoroutine("cor_CrossBannerAnim");
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
        if (GameManager.Instance.isVibration == true)
        {
            vibrationBtn.GetComponent<Image>().sprite = vibOn;
        }
        else
        {
            vibrationBtn.GetComponent<Image>().sprite = vibOff;
        }
    }

    public void Result()
    {
        if (newBestScoreImage.activeSelf == true)
        {
            newBestScoreImage.SetActive(false);
            resultNewBestScoreImage.SetActive(true);
        }
        resultScoreText.text = currScoreText.text;

        resultGoldText.text = string.Format("{0:#,##0}", rewardGold);
        resultGoldBtnText.text = resultGoldText.text;

        ResultUI.SetActive(true);
    }

    public void ViewAD()
    {
        GameManager.Instance.Gold += rewardGold;
        GameManager.Instance.ReStart();
    }
    IEnumerator cor_CrossBannerAnim()
    {
        while (true)
        {
            crossBanner.rotation = Quaternion.Euler(0, 0, 4.5f);
            yield return new WaitForSeconds(0.1f);
            crossBanner.rotation = Quaternion.Euler(0, 0, 3.0f);
            yield return new WaitForSeconds(0.1f);
            crossBanner.rotation = Quaternion.Euler(0, 0, 4.5f);
            yield return new WaitForSeconds(0.1f);
            crossBanner.rotation = Quaternion.Euler(0, 0, 3.0f);
            yield return new WaitForSeconds(0.1f);
            crossBanner.rotation = Quaternion.Euler(0, 0, 4.5f);
            yield return new WaitForSeconds(0.1f);
            crossBanner.rotation = Quaternion.Euler(0, 0, 3.0f);
            yield return new WaitForSeconds(0.1f);
            crossBanner.rotation = Quaternion.Euler(0, 0, 4.5f);
            yield return new WaitForSeconds(1.0f);
        }
    }
    public void OpenPlayStore()
    {
        Application.OpenURL("market://details?id=com.FakeWorld.Square");
    }
}
