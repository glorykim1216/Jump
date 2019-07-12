using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text judgement;
    private bool isjudging = false;

    public float gage = 100;
    public Image gagebar;
    public Text ui_Score;
    private float score;
    public Transform player;

    public Transform wall1;
    public Transform wall2;
    private float loopPosition;
    private float loopValue = 1000;
    private bool isMove = false;
    private void Start()
    {
        loopPosition = loopValue;
    }
    void Update()
    {
        score = player.position.z;
        ui_Score.text = ((int)score).ToString();
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
        gagebar.fillAmount = _value;
    }

    public void SetJudement(string _str)
    {
        if (isjudging == true)
            StopCoroutine("cor_JudgementTime");
        isjudging = true;
        judgement.text = _str;
        StartCoroutine("cor_JudgementTime");
    }

    IEnumerator cor_JudgementTime()
    {
        judgement.gameObject.SetActive(true);

        yield return new WaitForSeconds(1);
        isjudging = false;
        judgement.gameObject.SetActive(false);
    }
}
