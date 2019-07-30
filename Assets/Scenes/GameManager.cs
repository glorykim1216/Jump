using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    private int upPower;
    public int UpPower
    {
        get { return upPower; }
        set
        {
            upPower = value;
            DatabaseSave(isDBLoad);

        }
    }
    private int forwardPower;
    public int ForwardPower
    {
        get { return forwardPower; }
        set
        {
            forwardPower = value;
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

    private bool goldADState;
    public bool GoldADState
    {
        get { return goldADState; }
        set
        {
            goldADState = value;
        }
    }

    public int distance;
    public bool isGamePlaying = false;

    public int vibrationValue = 800;
    public bool isVibration = true;


    private void Awake()
    {
        Screen.SetResolution(720, 1280, true);
    
        DatabaseManager.Instance.Load();
        ADManager.Instance.init();
        Init();
        isDBLoad = true;

        //BestScore = 0;
        //Gold = Gold;
    }
    public new void Init()
    {
        distance = 0;

        isDBLoad = false;

        Gold = DatabaseManager.Instance.ItemList[0].gold;
        BestScore = DatabaseManager.Instance.ItemList[0].bestScore;
        OpenSkinList = DatabaseManager.Instance.ItemList[0].openSkinList;
        CurrSkin = DatabaseManager.Instance.ItemList[0].currSkin;
        UpPower = DatabaseManager.Instance.ItemList[0].upPower;
        ForwardPower = DatabaseManager.Instance.ItemList[0].forwardPower;

        isDBLoad = true;
    }
    // DB 저장
    public void DatabaseSave(bool _value)
    {
        if (_value)
            DatabaseManager.Instance.UpdateItemTable(gold, bestScore, openSkinList, currSkin, upPower, forwardPower,openEffectList,currEffect);
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
}
