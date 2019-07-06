using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
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
}
