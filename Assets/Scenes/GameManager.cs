using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoSingleton<GameManager>
{
    public UIManager UI_Manager;

    private bool isjudging = false;

    private bool isDBLoad = false;
    private int skin;
    public int Skin
    {
        get { return skin; }
        set
        {
            skin = value;
            //skin 배열 관련함수 필요
        }
    }
    private int gold;
    public int Gold
    {
        get { return gold; }
        set
        {
            gold = value;
            UI_Manager.goldText.text = gold.ToString() + " Gold";
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
            UI_Manager.bestScoreText.text = "Bset Score\n" + bestScore.ToString() + "m";
        }
    }

    public float gage = 100;

    private int distance;
    public Transform player;
    public bool isGamePlaying = false;

    public int vibrationValue = 1000;
    public bool isVibration = true;

    public Transform wall1;
    public Transform wall2;
    private float loopPosition;
    private float loopValue = 1000;
    private bool isMove = false;

    public Transform bestScoreWall;
    public override void Init()
    {
    }
    private void Start()
    {
        wall1 = GameObject.Find("wall1").transform;
        wall2 = GameObject.Find("wall2").transform;
        player = GameObject.Find("Player").transform;
        UI_Manager = GameObject.Find("Canvas").GetComponent<UIManager>();
        bestScoreWall = GameObject.Find("BestScoreWall").transform;
        bestScoreWall.gameObject.SetActive(false);

        loopPosition = loopValue;
        DatabaseManager.Instance.Load();

        Gold = DatabaseManager.Instance.ItemList[0].gold;
        BestScore = DatabaseManager.Instance.ItemList[0].bestScore;
        Skin = DatabaseManager.Instance.ItemList[0].skin;

        isDBLoad = true;

        bestScoreWall.position = new Vector3(bestScoreWall.position.x, bestScoreWall.position.y, bestScore);
    }
    // DB 저장
    void DatabaseSave(bool _value)
    {
        if (_value)
            DatabaseManager.Instance.UpdateItemTable(gold, bestScore, skin);
    }
    void Update()
    {
        if (isGamePlaying == false)
            return;

        // 현재 거리
        distance = (int)player.position.z;
        UI_Manager.currScoreText.text = distance.ToString();
        if (player.position.z > loopPosition)
            Move();

        if (distance >= bestScore - 100 && bestScoreWall.gameObject.activeSelf == false)
            bestScoreWall.gameObject.SetActive(true);
        // 신기록
        if (distance >= bestScore)
            NewBestScore();
    }
    // wall loop
    void Move()
    {
        loopPosition += loopValue;

        if (isMove == false)
            wall1.position = new Vector3(wall1.position.x, wall1.position.y, loopPosition);
        else
            wall2.position = new Vector3(wall2.position.x, wall2.position.y, loopPosition);

        isMove = !isMove;
    }
    // 게이지 출력
    public void SetGagebar(float _value)
    {
        UI_Manager.gagebar.fillAmount = _value;
    }
    // 판정 출력
    public void SetJudement(string _str)
    {
        if (isjudging == true)
            StopCoroutine("cor_JudgementTime");
        isjudging = true;
        UI_Manager.judgement.text = _str;
        StartCoroutine("cor_JudgementTime");
    }

    IEnumerator cor_JudgementTime()
    {
        UI_Manager.judgement.gameObject.SetActive(true);

        yield return new WaitForSeconds(1);
        isjudging = false;
        UI_Manager.judgement.gameObject.SetActive(false);
    }
    // 신기록
    public void NewBestScore()
    {
        BestScore = (int)distance;
        if (UI_Manager.newBestScore.activeSelf == false)
            UI_Manager.newBestScore.SetActive(true);
        if (UI_Manager.resultNewBestScore.activeSelf == false)
            UI_Manager.resultNewBestScore.SetActive(true);
    }
    public IEnumerator GameOver()
    {
        yield return new WaitForSeconds(3);
        isGamePlaying = false;

        UI_Manager.newBestScore.SetActive(false);

        UI_Manager.ResultUI.SetActive(true);
    }
    public void ReStart()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
