using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoSingleton<GameManager>
{
    private bool isjudging = false;

    private bool isDBLoad = false;

    private int gold;
    public int Gold
    {
        get { return gold; }
        set
        {
            gold = value;
            UIManager.Instance.goldText.text = gold.ToString() + " Gold";
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
            UIManager.Instance.bestScoreText.text = "Bset Score\n" + bestScore.ToString() + "m";
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

    public int distance;
    public Transform player;
    public bool isGamePlaying = false;

    public int vibrationValue = 800;
    public bool isVibration = true;


    private void Start()
    {
        Screen.SetResolution(720, 1280, true);

        player = GameObject.Find("Player").transform;



  
        DatabaseManager.Instance.Load();

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
            DatabaseManager.Instance.UpdateItemTable(gold, bestScore, openSkinList, currSkin, upPower, forwardPower);
    }
    void Update()
    {
        if (isGamePlaying == false)
            return;

        // 현재 거리
        UIManager.Instance.currScoreText.text = distance.ToString();
 
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
    public void SetJudement(string _str)
    {
        if (isjudging == true)
            StopCoroutine("cor_JudgementTime");
        isjudging = true;
        UIManager.Instance.judgement.text = _str;
        StartCoroutine("cor_JudgementTime");
    }

    IEnumerator cor_JudgementTime()
    {
        UIManager.Instance.judgement.gameObject.SetActive(true);

        yield return new WaitForSeconds(1);
        isjudging = false;
        UIManager.Instance.judgement.gameObject.SetActive(false);
    }
    // 신기록
    public void NewBestScore()
    {
        BestScore = (int)distance;
        if (UIManager.Instance.newBestScore.activeSelf == false)
            UIManager.Instance.newBestScore.SetActive(true);
        if (UIManager.Instance.resultNewBestScore.activeSelf == false)
            UIManager.Instance.resultNewBestScore.SetActive(true);
    }
    public IEnumerator GameOver()
    {
        isGamePlaying = false;

        yield return new WaitForSeconds(3);

        UIManager.Instance.newBestScore.SetActive(false);

        UIManager.Instance.ResultUI.SetActive(true);
    }
    public void ReStart()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
