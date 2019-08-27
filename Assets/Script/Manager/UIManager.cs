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

    public Button vibrationBtn;
    public Button resultGoldBtn;
    public Button resultGoldDoubleBtn;

    public Button instagramBtn;
    public Button privacyPolicyBtn;

    public Button jumpBtn;
    public Button speedBtn;
    public Button offlineGoldBtn;

    private Text jumpBtnLvText;
    private Text jumpBtnGoldText;
    private Text speedBtnLvText;
    private Text speedBtnGoldText;
    private Text offlineGoldBtnLvText;
    private Text offlineGoldBtnGoldText;

    public Text resultScoreText;
    public Text resultGoldText;
    public Text resultGoldBtnText;

    public Text countText;

    public GameObject SkinScrollViewObj;
    public GameObject EffectScrollViewObj;
    public Sprite vibOn;
    public Sprite vibOff;

    public GameObject judgementFailImage;
    public GameObject judgementGoodImage;
    public GameObject judgementExceImage;
    public GameObject newBestScoreImage;
    public GameObject resultNewBestScoreImage;
    public Text gold2CountDown;

    public List<int> skinBuyList;
    CheckSkin[] CheckSkinData;
    CheckEffect[] CheckEffectData;

    public GameObject BuyIMG;
    public GameObject BuyEffectIMG;
    public int priceTemp;
    public Texture renderTemp;
    public GameObject ObjTemp;
    public int SkinNumTemp;

    public RectTransform crossBanner;
    public GameObject skinEffectObj;
    public Scrollbar skinScrollBar;
    public Scrollbar effectScrollBar;
    GameObject SkinObj;
    GameObject EffectObj;


    public player Player;
    public override void Init() { }
    public ParticleSystem UiPs;
    ParticleSystem.MainModule UipsMain;
    ParticleSystem.ShapeModule UipsShape;

    public Slider audioVolume;

    public GameObject DB_Error;

    private void SkillBtn(eSkillBtn _value)
    {
        //SoundManager.Instance.PlaySound(eSound.button_UI_rise.ToString(), false, GameManager.Instance.AudioVolume);
        SoundManager.Instance.PlaySound(eSound.button_UI_rise,0);
        switch (_value)
        {
            case eSkillBtn.Jump:
                if (GameManager.Instance.Gold > int.Parse(SkillManager.Instance.GetValue(GameManager.Instance.UpPowerLevel).GOLD))
                {
                    GameManager.Instance.Gold -= int.Parse(SkillManager.Instance.GetValue(GameManager.Instance.UpPowerLevel).GOLD);
                    GameManager.Instance.UpPowerLevel++;
                    jumpBtnLvText.text = "Lv " + SkillManager.Instance.GetValue(GameManager.Instance.UpPowerLevel).KEY;
                    jumpBtnGoldText.text = SkillManager.Instance.GetValue(GameManager.Instance.UpPowerLevel).GOLD;
                    GameManager.Instance.SetJumpPower();
                }
                break;
            case eSkillBtn.Speed:
                if (GameManager.Instance.Gold > int.Parse(SkillManager.Instance.GetValue(GameManager.Instance.ForwardPowerLevel).GOLD))
                {
                    GameManager.Instance.Gold -= int.Parse(SkillManager.Instance.GetValue(GameManager.Instance.ForwardPowerLevel).GOLD);
                    GameManager.Instance.ForwardPowerLevel++;
                    speedBtnLvText.text = "Lv " + SkillManager.Instance.GetValue(GameManager.Instance.ForwardPowerLevel).KEY;
                    speedBtnGoldText.text = SkillManager.Instance.GetValue(GameManager.Instance.ForwardPowerLevel).GOLD;
                    GameManager.Instance.SetForwardPower();
                }
                break;
            case eSkillBtn.OfflineGold:
                if (GameManager.Instance.Gold > int.Parse(SkillManager.Instance.GetValue(GameManager.Instance.OfflineGoldLevel).GOLD))
                {
                    GameManager.Instance.Gold -= int.Parse(SkillManager.Instance.GetValue(GameManager.Instance.OfflineGoldLevel).GOLD);
                    GameManager.Instance.OfflineGoldLevel++;
                    offlineGoldBtnLvText.text = "Lv " + SkillManager.Instance.GetValue(GameManager.Instance.OfflineGoldLevel).KEY;
                    offlineGoldBtnGoldText.text = SkillManager.Instance.GetValue(GameManager.Instance.OfflineGoldLevel).GOLD;
                }
                break;
        }

    }
    void Start()
    {
        GameManager.Instance.Init();
        SoundManager.Instance.PlaySoundBackground();
        skinEffectObj.SetActive(false);
        SkinScrollViewObj.SetActive(true);
        EffectScrollViewObj.SetActive(true);
        vibrationBtn.onClick.AddListener(VibrationOption);
        resultGoldBtn.onClick.AddListener(ViewAD);
        crossBanner.GetComponent<Button>().onClick.AddListener(OpenPlayStore);
        instagramBtn.GetComponent<Button>().onClick.AddListener(() => { Application.OpenURL("https://www.instagram.com/gamenest_studio/"); });
        privacyPolicyBtn.GetComponent<Button>().onClick.AddListener(() => { Application.OpenURL("https://gameneststudio.webs.com/privacy-policy"); });
        audioVolume.onValueChanged.AddListener((float value) => { GameManager.Instance.AudioVolume = value; });
        audioVolume.value = GameManager.Instance.AudioVolume;

        jumpBtn.onClick.AddListener(() => SkillBtn(eSkillBtn.Jump));
        speedBtn.onClick.AddListener(() => SkillBtn(eSkillBtn.Speed));
        offlineGoldBtn.onClick.AddListener(() => SkillBtn(eSkillBtn.OfflineGold));

        jumpBtnLvText = jumpBtn.transform.Find("LevelText").GetComponent<Text>();
        jumpBtnGoldText = jumpBtn.transform.Find("GoldText").GetComponent<Text>();
        speedBtnLvText = speedBtn.transform.Find("LevelText").GetComponent<Text>();
        speedBtnGoldText = speedBtn.transform.Find("GoldText").GetComponent<Text>();
        offlineGoldBtnLvText = offlineGoldBtn.transform.Find("LevelText").GetComponent<Text>();
        offlineGoldBtnGoldText = offlineGoldBtn.transform.Find("GoldText").GetComponent<Text>();

        jumpBtnLvText.text = "Lv " + SkillManager.Instance.GetValue(GameManager.Instance.UpPowerLevel).KEY;
        jumpBtnGoldText.text = SkillManager.Instance.GetValue(GameManager.Instance.UpPowerLevel).GOLD;
        speedBtnLvText.text = "Lv " + SkillManager.Instance.GetValue(GameManager.Instance.ForwardPowerLevel).KEY;
        speedBtnGoldText.text = SkillManager.Instance.GetValue(GameManager.Instance.ForwardPowerLevel).GOLD;
        offlineGoldBtnLvText.text = "Lv " + SkillManager.Instance.GetValue(GameManager.Instance.OfflineGoldLevel).KEY;
        offlineGoldBtnGoldText.text = SkillManager.Instance.GetValue(GameManager.Instance.OfflineGoldLevel).GOLD;


        VibrationOnOffCheck();

        SkinObj = skinEffectObj.transform.Find("Skin").gameObject;
        EffectObj = skinEffectObj.transform.Find("Effect").gameObject;
        //GameManager.Instance.OpenSkinList = 231;
        //Debug.Log("GameManager.Instance.OpenSkinList" + GameManager.Instance.OpenSkinList);
        SkinInit();
        EffectInit();
        BuyIMG.SetActive(false);
        BuyEffectIMG.SetActive(false);
        SkinScrollViewObj.SetActive(false);
        EffectScrollViewObj.SetActive(false);

  
        UipsMain = UiPs.main;
        UipsShape = UiPs.shape;
        UipsMain.loop = true;

        StartCoroutine("cor_CrossBannerAnim");
    }
    public void SetDB_Error()
    {
        DB_Error.SetActive(true);
    }
    void SkinInit()
    {
        string test;
        test = System.Convert.ToString(GameManager.Instance.OpenSkinList, 2).PadLeft(20, '0');
        //Debug.Log(test);
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
                //Debug.Log("!!!!");
                CheckSkinData[i].moneyText.color = Color.yellow;
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

        GameObject.Find("Player").GetComponentInChildren<SkinnedMeshRenderer>().material = Resources.Load("Material/Icem" + GameManager.Instance.CurrSkin.ToString()) as Material;

    }
    void EffectInit()
    {
        string test;
        test = System.Convert.ToString(GameManager.Instance.OpenEffectList, 2).PadLeft(20, '0');
        //Debug.Log(test);
        CheckEffectData = this.gameObject.GetComponentsInChildren<CheckEffect>();

        foreach (CheckEffect k in CheckEffectData)
        {
            k.CheckImg.SetActive(false);
        }

        for (int i = 0; i < CheckEffectData.Length; i++)
        {
            CheckEffectData[i].SkinNumber = i + 1;
            if (test[i].ToString() == "1")
            {
                //Debug.Log("!!!!");
                CheckEffectData[i].moneyText.color = Color.yellow;
                CheckEffectData[i].moneyText.text = "OK        ";
                CheckEffectData[i].moneyImg.enabled = false;
                CheckEffectData[i].BuyCheck = true;

            }
            else
            {
                CheckEffectData[i].BuyCheck = false;
            }

            if (CheckEffectData[i].SkinNumber == GameManager.Instance.CurrEffect)
                CheckEffectData[i].CheckImg.SetActive(true);
        }

        MeshRenderer[] rs = GameObject.Find("PlayerWing").GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer r in rs)
        {
            r.material = Resources.Load("Material/Wing_Sub " + GameManager.Instance.CurrEffect.ToString()) as Material;
        }
    }
    string Num;
    // Update is called once per frame
    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.A))
    //    {
    //        InitAllData();
    //    }
    //}
    public void InitAllData()
    {
        //CheckSkinData = this.gameObject.GetComponentsInChildren<CheckSkin>();

        //Num = null;
        //for (int i = 0; i < CheckSkinData.Length; i++)
        //{
        //    if (i == 0)
        //        CheckSkinData[i].BuyCheck = true;
        //    else
        //        CheckSkinData[i].BuyCheck = false;
        //}


        //foreach (CheckSkin k in CheckSkinData)
        //{
        //    if (k.BuyCheck)
        //        Num += "1";
        //    else
        //        Num += "0";
        //}
        ///////
        ////저장
        //GameManager.Instance.OpenSkinList = System.Convert.ToInt32(Num, 2);
        //string test;
        //test = System.Convert.ToString(GameManager.Instance.OpenSkinList, 2).PadLeft(20, '0');

        //CheckEffectData = this.gameObject.GetComponentsInChildren<CheckEffect>();

        //Num = null;
        //for (int i = 0; i < CheckEffectData.Length; i++)
        //{
        //    if (i == 0)
        //        CheckEffectData[i].BuyCheck = true;
        //    else
        //        CheckEffectData[i].BuyCheck = false;
        //}


        //foreach (CheckEffect k in CheckEffectData)
        //{
        //    if (k.BuyCheck)
        //        Num += "1";
        //    else
        //        Num += "0";
        //}
        /////
        //저장
        //GameManager.Instance.OpenEffectList = System.Convert.ToInt32(Num, 2);
        //string test2;
        //test2 = System.Convert.ToString(GameManager.Instance.OpenEffectList, 2).PadLeft(20, '0');


        GameManager.Instance.CurrEffect = 1;
        GameManager.Instance.CurrSkin = 1;
        GameManager.Instance.Gold = 0;
        GameManager.Instance.BestScore = 0;
        GameManager.Instance.UpPowerLevel = 1;
        GameManager.Instance.ForwardPowerLevel = 1;
        GameManager.Instance.OfflineGoldLevel = 1;
    }

    public void OptimizeSkin()
    {
        for (int i = 0; i < 5; i++)
        {
            if (skinScrollBar.value <= 1 - (0.2f * i) && skinScrollBar.value > 1 - (0.2f * (i + 1)))
            {

                for (int j = 0; j < 20; j++)
                {
                    //.transform.GetChild( j).gameObject.SetActive(false);
                    SkinObj.transform.GetChild(j).GetComponent<Camera>().enabled = false;
                }
                for (int j = 0; j < 9; j++)
                {
                    //SkinObj.transform.GetChild((i*3) +j).gameObject.SetActive(true);
                    SkinObj.transform.GetChild((i * 3) + j).GetComponent<Camera>().enabled = true;
                    // SkinObj.transform.GetChild(1).name
                }
            }
        }
    }

    public void OptimizeEffect()
    {
        for (int i = 0; i < 5; i++)
        {
            if (effectScrollBar.value <= 1 - (0.2f * i) && effectScrollBar.value > 1 - (0.2f * (i + 1)))
            {

                for (int j = 0; j < 20; j++)
                {
                    //EffectObj.transform.GetChild(j).gameObject.SetActive(false);
                    EffectObj.transform.GetChild(j).GetComponent<Camera>().enabled = false;
                }
                for (int j = 0; j < 9; j++)
                {
                    //EffectObj.transform.GetChild((i * 3) + j).gameObject.SetActive(true);
                    EffectObj.transform.GetChild((i * 3) + j).GetComponent<Camera>().enabled = true;
                    // SkinObj.transform.GetChild(1).name
                }
            }
        }
    }

    public void FreeSkin()
    {
        SoundManager.Instance.PlayPause();
        SoundManager.Instance.PlaySound(eSound.button_UI,0);
        //SoundManager.Instance.PlaySound(eSound.button_UI.ToString(), false, GameManager.Instance.AudioVolume);
        GameManager.Instance.SkinADState = true;
        ADManager.Instance.ShowRewardAd();


        BuyIMG.SetActive(false);
        SkinScrollViewObj.SetActive(false);
    }

    public void FreeEffect()
    {
        SoundManager.Instance.PlayPause();
        SoundManager.Instance.PlaySound(eSound.button_UI,0);
        //SoundManager.Instance.PlaySound(eSound.button_UI.ToString(), false, GameManager.Instance.AudioVolume);
        GameManager.Instance.EffectADState = true;
        ADManager.Instance.ShowRewardAd();


        BuyEffectIMG.SetActive(false);
        EffectScrollViewObj.SetActive(false);
    }

    public void GoldViewAD()
    {
        SoundManager.Instance.PlayPause();
        SoundManager.Instance.PlaySound(eSound.button_UI,0);
        // SoundManager.Instance.PlaySound(eSound.button_UI.ToString(), false, GameManager.Instance.AudioVolume);
        GameManager.Instance.GoldADState = true;
        ADManager.Instance.ShowRewardAd();
        DatabaseManager.Instance.UpdateItemTable(GameManager.Instance.BestScore);
        GameManager.Instance.ReStart();
    }

    public void CheckSkin(int CurrentSkin)
    {
        SoundManager.Instance.PlaySound(eSound.button_UI,0);
        //SoundManager.Instance.PlaySound(eSound.button_UI.ToString(), false, GameManager.Instance.AudioVolume);
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
            renderTemp = EventSystem.current.currentSelectedGameObject.transform.parent.GetComponentsInChildren<Image>()[1].mainTexture;
            ObjTemp = EventSystem.current.currentSelectedGameObject;
            GameManager.Instance.PlayerMat = GameObject.Find("Player").GetComponentInChildren<SkinnedMeshRenderer>().material;
            SkinNumTemp = CurrentSkin;
            GameManager.Instance.TempMat = Resources.Load("Material/Icem" + SkinNumTemp.ToString()) as Material;


            BuyIMG.SetActive(true);
            //BuyIMG.transform.Find("price").GetComponent<Text>().text = EventSystem.current.currentSelectedGameObject.GetComponent<CheckSkin>().needMoney.ToString();


        }
        //Debug.Log("CurrentSkin : " + GameManager.Instance.CurrSkin);
        //Debug.Log("priceTemp : " + priceTemp);
        //Debug.Log(EventSystem.current.currentSelectedGameObject.name);
    }

    public void CheckEffect(int CurrentSkin)
    {
        SoundManager.Instance.PlaySound(eSound.button_UI,0);
        //SoundManager.Instance.PlaySound(eSound.button_UI.ToString(), false, GameManager.Instance.AudioVolume);
        //Debug.Log("priceTemp : ");
        if (EventSystem.current.currentSelectedGameObject.GetComponent<CheckEffect>().BuyCheck)
        {
            foreach (CheckEffect k in CheckEffectData)
            {
                k.CheckImg.SetActive(false);
            }
            GameManager.Instance.CurrEffect = CurrentSkin;

            EventSystem.current.currentSelectedGameObject.GetComponent<CheckEffect>().CheckImg.SetActive(true);

            //구입후 이미지 변환 작성해야됨;
            MeshRenderer[] rs = GameObject.Find("PlayerWing").GetComponentsInChildren<MeshRenderer>();
            foreach (MeshRenderer r in rs)
            {
                r.material = Resources.Load("Material/Wing_Sub " + GameManager.Instance.CurrEffect.ToString()) as Material;
            }
        }
        else
        {
            priceTemp = EventSystem.current.currentSelectedGameObject.GetComponent<CheckEffect>().needMoney;
            renderTemp = EventSystem.current.currentSelectedGameObject.transform.parent.GetComponentInChildren<RawImage>().texture;
            ObjTemp = EventSystem.current.currentSelectedGameObject;
            GameManager.Instance.PlayerMat = GameObject.Find("PlayerWing").GetComponentInChildren<MeshRenderer>().material;
            SkinNumTemp = CurrentSkin;
            GameManager.Instance.TempMat = Resources.Load("Material/Wing_Sub " + SkinNumTemp.ToString()) as Material;


            BuyEffectIMG.SetActive(true);

            if (CurrentSkin == 20)
            {
                BuyEffectIMG.GetComponentInChildren<Price>().GetComponentsInChildren<RawImage>()[1].color = new Color(1, 1, 1, 1);
                BuyEffectIMG.GetComponentInChildren<Price>().GetComponentsInChildren<RawImage>()[1].texture = renderTemp;
            }
            else
                BuyEffectIMG.GetComponentInChildren<Price>().GetComponentsInChildren<RawImage>()[1].color = new Color(1, 1, 1, 0);

        }
        //Debug.Log("CurrentSkin : " + GameManager.Instance.CurrEffect);
        //Debug.Log("priceTemp : " + priceTemp);
        //Debug.Log(EventSystem.current.currentSelectedGameObject.name);
    }

    public void Purchase()
    {
        SoundManager.Instance.PlaySound(eSound.button_UI, 0);
        //SoundManager.Instance.PlaySound(eSound.button_UI.ToString(), false, GameManager.Instance.AudioVolume);
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
            //Debug.Log(test);

            foreach (CheckSkin k in CheckSkinData)
            {

                k.CheckImg.SetActive(false);

            }

            for (int i = 0; i < CheckSkinData.Length; i++)
            {
                CheckSkinData[i].SkinNumber = i + 1;
                if (test[i].ToString() == "1")
                {
                    //Debug.Log("!!!!");
                    CheckSkinData[i].moneyText.color = Color.yellow;
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
    public void PurchaseEffect()
    {
        SoundManager.Instance.PlaySound(eSound.button_UI, 0);
        //SoundManager.Instance.PlaySound(eSound.button_UI.ToString(), false, GameManager.Instance.AudioVolume);
        if (GameManager.Instance.Gold > ObjTemp.GetComponent<CheckEffect>().needMoney)
        {
            GameManager.Instance.Gold -= ObjTemp.GetComponent<CheckEffect>().needMoney;

            ObjTemp.GetComponent<CheckEffect>().BuyCheck = true;

            Num = null;
            foreach (CheckEffect k in CheckEffectData)
            {
                if (k.BuyCheck)
                    Num += "1";
                else
                    Num += "0";
            }
            /////
            //저장
            GameManager.Instance.OpenEffectList = System.Convert.ToInt32(Num, 2);
            string test;
            test = System.Convert.ToString(GameManager.Instance.OpenEffectList, 2).PadLeft(20, '0');
            //Debug.Log(test);

            foreach (CheckEffect k in CheckEffectData)
            {

                k.CheckImg.SetActive(false);
                //Debug.Log(k.CheckImg.name);
            }

            for (int i = 0; i < CheckEffectData.Length; i++)
            {
                CheckEffectData[i].SkinNumber = i + 1;
                if (test[i].ToString() == "1")
                {
                    //Debug.Log("!!!!");
                    CheckEffectData[i].moneyText.color = Color.yellow;
                    CheckEffectData[i].moneyText.text = "OK        ";
                    CheckEffectData[i].moneyImg.enabled = false;
                    CheckEffectData[i].BuyCheck = true;

                }
                else
                {
                    CheckEffectData[i].BuyCheck = false;
                }

                if (CheckEffectData[i].SkinNumber == GameManager.Instance.CurrEffect)
                    CheckEffectData[i].CheckImg.SetActive(true);
            }

            BuyEffectIMG.SetActive(false);
            ////
            //Debug.Log("GameManager.Instance.CurrEffect" + GameManager.Instance.CurrEffect);

            foreach (CheckEffect k in CheckEffectData)
            {
                k.CheckImg.SetActive(false);
            }
            GameManager.Instance.CurrEffect = SkinNumTemp;

            ObjTemp.GetComponent<CheckEffect>().CheckImg.SetActive(true);


            MeshRenderer[] rs = GameObject.Find("PlayerWing").GetComponentsInChildren<MeshRenderer>();
            foreach (MeshRenderer r in rs)
            {
                r.material = Resources.Load("Material/Wing_Sub " + GameManager.Instance.CurrEffect.ToString()) as Material;
            }
            //구입후 이미지 변환 작성해야됨;
            //GameObject.Find("Player").GetComponentInChildren<SkinnedMeshRenderer>().material = Resources.Load("Material/Icem" + GameManager.Instance.CurrSkin.ToString()) as Material;
        }
    }

    public void SkinButton()
    {
        SoundManager.Instance.PlaySound(eSound.button_UI, 0);
        //SoundManager.Instance.PlaySound(eSound.button_UI.ToString(), false, GameManager.Instance.AudioVolume);
        if (!SkinScrollViewObj.activeSelf)
        {
            skinEffectObj.SetActive(false);
            SkinScrollViewObj.SetActive(true);
            skinEffectObj.transform.Find("Skin").gameObject.SetActive(false);
            skinEffectObj.transform.Find("Effect").gameObject.SetActive(false);

            skinScrollBar.value = 1;
            for (int i = 0; i < 5; i++)
            {
                if (skinScrollBar.value <= 1 - (0.2f * i) && skinScrollBar.value > 1 - (0.2f * (i + 1)))
                {

                    for (int j = 0; j < 20; j++)
                    {
                        //SkinObj.transform.GetChild(j).gameObject.SetActive(false);
                        SkinObj.transform.GetChild(j).GetComponent<Camera>().enabled = false;
                    }
                    for (int j = 0; j < 9; j++)
                    {
                        //SkinObj.transform.GetChild((i * 3) + j).gameObject.SetActive(true);
                        SkinObj.transform.GetChild((i * 3) + j).GetComponent<Camera>().enabled = true;
                        // SkinObj.transform.GetChild(1).name
                    }
                }
            }

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
        SoundManager.Instance.PlaySound(eSound.button_UI, 0);
        //SoundManager.Instance.PlaySound(eSound.button_UI.ToString(), false, GameManager.Instance.AudioVolume);
        BuyIMG.SetActive(false);
        BuyEffectIMG.SetActive(false);
    }
    public void EffectButton()
    {
        SoundManager.Instance.PlaySound(eSound.button_UI, 0);
        //SoundManager.Instance.PlaySound(eSound.button_UI.ToString(), false, GameManager.Instance.AudioVolume);
        if (!EffectScrollViewObj.activeSelf)
        {


            skinEffectObj.SetActive(true);
            EffectScrollViewObj.SetActive(true);
            skinEffectObj.transform.Find("Skin").gameObject.SetActive(false);
            skinEffectObj.transform.Find("Effect").gameObject.SetActive(true);
            effectScrollBar.value = 1;
            for (int i = 0; i < 5; i++)
            {
                if (effectScrollBar.value <= 1 - (0.2f * i) && effectScrollBar.value > 1 - (0.2f * (i + 1)))
                {

                    for (int j = 0; j < 20; j++)
                    {
                        //EffectObj.transform.GetChild(j).gameObject.SetActive(false);
                        EffectObj.transform.GetChild(j).GetComponent<Camera>().enabled = false;
                    }
                    for (int j = 0; j < 9; j++)
                    {
                        //EffectObj.transform.GetChild((i * 3) + j).gameObject.SetActive(true);
                        EffectObj.transform.GetChild((i * 3) + j).GetComponent<Camera>().enabled = true;
                        // SkinObj.transform.GetChild(1).name
                    }
                }
            }
        }
        else
        {
            BuyEffectIMG.SetActive(false);
            EffectScrollViewObj.SetActive(false);
            skinEffectObj.SetActive(false);
        }

    }

    public void SkinSelectedValue()
    {
        //Debug.Log("작동 잘됨");
    }
    // 옵션창 On
    public void OpenOption()
    {
        SoundManager.Instance.PlaySound(eSound.button_UI, 0);
        //SoundManager.Instance.PlaySound(eSound.button_UI.ToString(), false, GameManager.Instance.AudioVolume);
        OptionPopup.SetActive(true);
    }
    // 옵션창 Off
    public void CloseOption()
    {
        SoundManager.Instance.PlaySound(eSound.button_UI, 0);
        //SoundManager.Instance.PlaySound(eSound.button_UI.ToString(), false, GameManager.Instance.AudioVolume);
        OptionPopup.SetActive(false);
    }
    // 게임시작
    public void StartGame()
    {
        StopCoroutine("cor_CrossBannerAnim");
        LobbyUI.SetActive(false);
        InGameUI.SetActive(true);
        StartCoroutine("cor_StartCount");
        Player.Init();
    }
    // 진동 옵션
    public void VibrationOption()
    {
        SoundManager.Instance.PlaySound(eSound.button_UI, 0);
        GameManager.Instance.IsVibration = !GameManager.Instance.IsVibration;
        //SoundManager.Instance.PlaySound(eSound.button_UI.ToString(), false, GameManager.Instance.AudioVolume);
        VibrationOnOffCheck();
    }
    // 진동 On/Off 체크
    public void VibrationOnOffCheck()
    {
       
        if (GameManager.Instance.IsVibration == true)
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

        resultGoldText.text = string.Format("{0:#,##0}", GameManager.Instance.RewardGold);
        resultGoldBtnText.text = resultGoldText.text;

        ResultUI.SetActive(true);
        gold2CountDown.enabled = true;
        StartCoroutine(cor_Gold2CountDown());
    }

    public void ViewAD()
    {
        SoundManager.Instance.PlayPause();
        SoundManager.Instance.PlaySound(eSound.button_UI, 0);
        //SoundManager.Instance.PlaySound(eSound.button_UI.ToString(), false, GameManager.Instance.AudioVolume);
        GameManager.Instance.Gold += GameManager.Instance.RewardGold;
      
        if (GameManager.Instance.ADVideoCount % 4 == 3)
        {
            ADManager.Instance.ShowRewardedAd();
        }
        else
        {
            ADManager.Instance.ShowInterstitialAd(); 
        }
        GameManager.Instance.ADVideoCount++;
        DatabaseManager.Instance.UpdateItemTable(GameManager.Instance.BestScore);
        GameManager.Instance.ReStart();
        //Debug.Log("GameManager.Instance.ShowRewardedAd" + GameManager.Instance.ADVideoCount);

    }

    IEnumerator cor_Gold2CountDown()
    {
        float time = 4.0f;
        bool isCountDown = true;
        while (isCountDown)
        {
            time -= Time.deltaTime;
            gold2CountDown.text = ((int)time).ToString();
            if (time < 1)
            {
                gold2CountDown.enabled = false;
                //resultGoldDoubleBtn.gameObject.SetActive(false);
                isCountDown = false;

                UipsMain.loop = false;
                UipsShape.sphericalDirectionAmount = 1f;

            }

            yield return null;
        }

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

    IEnumerator cor_StartCount()
    {
        countText.text = "3";
        yield return new WaitForSeconds(1.0f);
        countText.text = "2";
        yield return new WaitForSeconds(1.0f);
        countText.text = "1";
        yield return new WaitForSeconds(1.0f);
        countText.text = "Go!";
        yield return new WaitForSeconds(1.0f);
        countText.gameObject.SetActive(false);
        GameManager.Instance.isGamePlaying = true;
    }
}
