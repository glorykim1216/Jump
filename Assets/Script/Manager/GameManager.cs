using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoSingleton<GameManager>
{
    private eJudgement currJudgement = eJudgement.None;
    public bool isjudging = false;

    private bool isDBLoad = false;

    private int gold;
    public int Gold
    {
        get { return gold; }
        set
        {
            gold = value;
            UIManager.Instance.goldText.text = string.Format("{0:#,##0}", gold);
            DatabaseSave(isDBLoad);
        }
    }
    private int bestScore;
    public int BestScore
    {
        get { return bestScore; }
        set
        {
            bestScore = value;
            UIManager.Instance.bestScoreText.text = string.Format("{0:#,##0}", bestScore);
        }
    }
    private int openSkinList;
    public int OpenSkinList
    {
        get { return openSkinList; }
        set
        {
            openSkinList = value;
            //skin 배열 관련함수 필요
            DatabaseSave(isDBLoad);
        }
    }
    private int currSkin;
    public int CurrSkin
    {
        get { return currSkin; }
        set
        {
            currSkin = value;
            DatabaseSave(isDBLoad);

        }
    }
    private int upPowerLevel;
    public int UpPowerLevel
    {
        get { return upPowerLevel; }
        set
        {
            upPowerLevel = value;
            DatabaseSave(isDBLoad);

        }
    }
    private int forwardPowerLevel;
    public int ForwardPowerLevel
    {
        get { return forwardPowerLevel; }
        set
        {
            forwardPowerLevel = value;
            DatabaseSave(isDBLoad);

        }
    }
    private int offlineGoldLevel;
    public int OfflineGoldLevel
    {
        get { return offlineGoldLevel; }
        set
        {
            offlineGoldLevel = value;
            DatabaseSave(isDBLoad);
        }
    }

    private int openEffectList;
    public int OpenEffectList
    {
        get { return openEffectList; }
        set
        {
            openEffectList = value;
            //skin 배열 관련함수 필요
            DatabaseSave(isDBLoad);
        }
    }
    private int currEffect;
    public int CurrEffect
    {
        get { return currEffect; }
        set
        {
            currEffect = value;
            DatabaseSave(isDBLoad);

        }
    }
    private Material playerMat;
    public Material PlayerMat
    {
        get { return playerMat; }
        set
        {
            playerMat = value;
        }
    }


    private Material tempMat;
    public Material TempMat
    {
        get { return tempMat; }
        set
        {
            tempMat = value;
        }
    }

    private int rewardGold;
    public int RewardGold
    {
        get { return rewardGold; }
        set
        {
            rewardGold = value;
        }
    }

    public float rewardGoldRate = 0.5f;

    private bool skinADState;
    public bool SkinADState
    {
        get { return skinADState; }
        set
        {
            skinADState = value;
        }
    }

    private bool effectADState;
    public bool EffectADState
    {
        get { return effectADState; }
        set
        {
            effectADState = value;
        }
    }

    private bool goldADState;
    public bool GoldADState
    {
        get { return goldADState; }
        set
        {
            goldADState = value;
        }
    }

    private int aDVideoCount;
    public int ADVideoCount
    {
        get { return aDVideoCount; }
        set
        {
            aDVideoCount = value;
        }
    }

    private string saveDateTime = "0";
    public string SaveDateTime
    {
        get { return saveDateTime; }
        set
        {
            saveDateTime = value;
        }
    }

    public int distance;
    public bool isGamePlaying = false;

    public int vibrationValue = 800;
    private bool isVibration = true;
    public bool IsVibration
    {
        get { return isVibration; }
        set
        {
            isVibration = value;
            DatabaseManager.Instance.UpdateItemTable(audioVolume.ToString(), isVibration ? 1 : 0);
        }
    }

    public float forwardPower = 2;
    public float jumpPower = 10;
    public float halfLife = 0.5f;

    private float audioVolume = 1;
    public float AudioVolume
    {
        get { return audioVolume; }
        set
        {
            audioVolume = value;
            SoundManager.Instance.SetAllVolume(audioVolume);
            DatabaseManager.Instance.UpdateItemTable(audioVolume.ToString(), IsVibration ? 1 : 0);
        }
    }
    private string deviceID;


    void Awake()
    {
        Screen.SetResolution(Screen.width, Screen.width / 9 * 16, true);



        isDBLoad = DatabaseManager.Instance.Load();
        Init();



        SoundManager.Instance.LoadSound();
        ADManager.Instance.init();
        Gold += GetOfflineGold();

        SoundManager.Instance.PlaySound(eSound.BGM.ToString(), true, audioVolume);
        //SoundManager.Instance.PlaySound(eSound.BGM, 1, audioVolume);
        //SoundManager.Instance.PlaySound(eSound.BGM, 1);

        SkillManager.Instance.LoadJson();
        // JSON_TEST_CODE
        //SkillInfo skillInfo = SkillManager.Instance.GetValue(277);
        //Debug.Log(skillInfo.KEY + ":" + skillInfo.GOLD);

        //BestScore = 0;
        //Gold = Gold;
    }
    private void Start()
    {
        if (PlayerInit() == false)
        {
            UIManager.Instance.SetDB_Error();
        }
    }
    public new void Init()
    {
        distance = 0;

        isDBLoad = false;

        if (DatabaseManager.Instance.ItemList != null)
        {

            Gold = DatabaseManager.Instance.ItemList.gold;
            BestScore = DatabaseManager.Instance.ItemList.bestScore;
            openSkinList = DatabaseManager.Instance.ItemList.openSkinList;
            currSkin = DatabaseManager.Instance.ItemList.currSkin;
            upPowerLevel = DatabaseManager.Instance.ItemList.upPowerLevel;
            forwardPowerLevel = DatabaseManager.Instance.ItemList.forwardPowerLevel;
            offlineGoldLevel = DatabaseManager.Instance.ItemList.offlineGoldLevel;
            openEffectList = DatabaseManager.Instance.ItemList.openEffectList;
            currEffect = DatabaseManager.Instance.ItemList.currEffect;
            saveDateTime = DatabaseManager.Instance.ItemList.dateTime;
            isVibration = (DatabaseManager.Instance.ItemList.vibration == 1) ? true : false;
            audioVolume = float.Parse(DatabaseManager.Instance.ItemList.soundVolume);
            deviceID = DatabaseManager.Instance.ItemList.deviceID;
            isDBLoad = true;

            SetJumpPower();
            SetForwardPower();

        }


    }

    public bool PlayerInit()
    {
        //PlayerPrefs.DeleteAll();
        //초기 DB뽑기

        if (DatabaseManager.Instance.ItemList.deviceID == "0")
        {
            //DB 테이블 생성
            DatabaseManager.Instance.CreateTable();
            //DB 날짜 초기화
            DatabaseManager.Instance.UpdateItemTable(DateTime.Now.ToString("yyyyMMddHHmmss"));
            //디바이스 아이디 등록
            string ID = SystemInfo.deviceUniqueIdentifier;

            DatabaseManager.Instance.UpdateItemTable_DeviceID(ID);

            //ui skin effect gold score level 초기화
            UIManager.Instance.InitAllData();

            PlayerPrefs.SetString("DeviceID", ID);
            PlayerPrefs.Save();
        }
        else
        {
            //최종빌드시 아래 한줄 살려놧따가 지워야함 DeviceID "0"으로 초기화;
            //DatabaseManager.Instance.UpdateItemTable_DeviceID("0");

            isDBLoad = DatabaseManager.Instance.Load();
            Init();

            // DB 해킹 의심
            if (deviceID != PlayerPrefs.GetString("DeviceID"))
            {
                return false;
            }
        }

        //PlayerPrefs.DeleteAll();

        return true;
    }
    public void SetJumpPower()
    {
        jumpPower = 10 + (17.0f / 300.0f) * (float)UpPowerLevel;
    }
    public void SetForwardPower()
    {
        forwardPower = 2 + (48.0f / 300.0f) * (float)ForwardPowerLevel;

    }
    // DB 저장
    public void DatabaseSave(bool _value)
    {
        if (_value)
            DatabaseManager.Instance.UpdateItemTable(gold, bestScore, openSkinList, currSkin, upPowerLevel, forwardPowerLevel, offlineGoldLevel, openEffectList, currEffect);
    }
    void Update()
    {
        if (isGamePlaying == false)
            return;

        // 현재 거리
        UIManager.Instance.currScoreText.text = string.Format("{0:#,##0}", distance) + "m";

        // 신기록
        if (distance >= bestScore)
            NewBestScore();
    }

    // 게이지 출력
    public void SetGagebar(float _value)
    {
        UIManager.Instance.gagebar.fillAmount = _value;
    }
    // 판정 출력
    public void SetJudgement(eJudgement _judgement)
    {
        currJudgement = _judgement;
        if (isjudging == true)
        {
            StopCoroutine("cor_JudgementTime");
            UIManager.Instance.judgementFailImage.SetActive(false);
            UIManager.Instance.judgementGoodImage.SetActive(false);
            UIManager.Instance.judgementExceImage.SetActive(false);
        }
        isjudging = true;
        StartCoroutine("cor_JudgementTime");
    }

    IEnumerator cor_JudgementTime()
    {
        if (currJudgement == eJudgement.Fail)
            UIManager.Instance.judgementFailImage.SetActive(true);
        else if (currJudgement == eJudgement.Good)
            UIManager.Instance.judgementGoodImage.SetActive(true);
        else if (currJudgement == eJudgement.Excellent)
            UIManager.Instance.judgementExceImage.SetActive(true);

        yield return new WaitForSeconds(1);

        isjudging = false;
        if (currJudgement == eJudgement.Fail)
            UIManager.Instance.judgementFailImage.SetActive(false);
        else if (currJudgement == eJudgement.Good)
            UIManager.Instance.judgementGoodImage.SetActive(false);
        else if (currJudgement == eJudgement.Excellent)
            UIManager.Instance.judgementExceImage.SetActive(false);
    }
    // 신기록
    public void NewBestScore()
    {
        BestScore = (int)distance;
        if (UIManager.Instance.newBestScoreImage.activeSelf == false)
            UIManager.Instance.newBestScoreImage.SetActive(true);
        if (UIManager.Instance.resultNewBestScoreImage.activeSelf == false)
            UIManager.Instance.resultNewBestScoreImage.SetActive(true);
    }
    public IEnumerator GameOver()
    {
        isGamePlaying = false;

        yield return new WaitForSeconds(1f);

        RewardGold = (int)(distance * rewardGoldRate);

        UIManager.Instance.Result();
    }
    public void ReStart()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public int GetOfflineGold()
    {
        if (saveDateTime == "0")
            return 0;

        DateTime oldTime = DateTime.ParseExact(saveDateTime, "yyyyMMddHHmmss", System.Globalization.CultureInfo.InstalledUICulture);
        TimeSpan span = DateTime.Now - oldTime;

        // 분 단위로 변경
        int temp = (int)(span.TotalSeconds / 60);
        // 최대 120분
        temp = temp > 120 ? 120 : temp;
        temp *= offlineGoldLevel;
        return temp;
    }
}
