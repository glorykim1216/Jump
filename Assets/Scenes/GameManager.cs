using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            DatabaseSave(isDBLoad);
        }
    }
    private int gold;
    public int Gold
    {
        get { return gold; }
        set
        {
            gold = value;
            UI_Manager.ui_gold.text = gold.ToString() + " Gold";
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
            UI_Manager.ui_bestScore.text = "Bset Score\n" + bestScore.ToString() + "m";
            DatabaseSave(isDBLoad);
        }
    }

    public float gage = 100;

    private float distance;
    public Transform player;
    public bool isGamePlaying = false;

    public int vibrationValue;

    public Transform wall1;
    public Transform wall2;
    private float loopPosition;
    private float loopValue = 1000;
    private bool isMove = false;

    private void Start()
    {
        wall1 = GameObject.Find("wall1").transform;
        wall2 = GameObject.Find("wall2").transform;
        player = GameObject.Find("Player").transform;
        //UI_Manager = GameObject.Find("Canvas").GetComponent<UIManager>();

        loopPosition = loopValue;
        DatabaseManager.Instance.Load();

        Gold = DatabaseManager.Instance.ItemList[0].gold;
        BestScore = DatabaseManager.Instance.ItemList[0].bestScore;
        Skin = DatabaseManager.Instance.ItemList[0].skin;

        isDBLoad = true;
    }
    void DatabaseSave(bool _value)
    {
        if (_value)
            DatabaseManager.Instance.UpdateItemTable(gold, bestScore, skin);
    }
    void Update()
    {
        if (isGamePlaying == false)
            return;

        distance = player.position.z;
        UI_Manager.ui_Score.text = ((int)distance).ToString();
        if (player.position.z > loopPosition)
            Move();
    }
    void Move()
    {
        loopPosition += loopValue;

        if (isMove == false)
            wall1.position = new Vector3(wall1.position.x, wall1.position.y, loopPosition);
        else
            wall2.position = new Vector3(wall2.position.x, wall2.position.y, loopPosition);

        isMove = !isMove;
    }

    public void SetGagebar(float _value)
    {
        UI_Manager.gagebar.fillAmount = _value;
    }

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
}
