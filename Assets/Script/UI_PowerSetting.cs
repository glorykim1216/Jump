using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI_PowerSetting : MonoBehaviour
{
    public player player;
    public GameObject powerSetting;
    public InputField halfLite;
    public InputField jump;
    public InputField forward;
    public InputField mass;
    public InputField gold;
    public InputField vibration;

    void Start()
    {  
        halfLite.text = player.halfLife.ToString();
        jump.text = player.jumpPower.ToString();
        forward.text = player.forwardPower.ToString();
        mass.text = player.mass.ToString();
        gold.text = GameManager.Instance.rewardGoldRate.ToString();
        vibration.text = GameManager.Instance.vibrationValue.ToString();
    }

    public void ReStart()
    {
        player.halfLife = float.Parse(halfLite.text);
        player.jumpPower = float.Parse(jump.text);
        player.forwardPower = float.Parse(forward.text);
        player.mass = float.Parse(mass.text);
        GameManager.Instance.rewardGoldRate = float.Parse(gold.text);
        GameManager.Instance.vibrationValue = int.Parse(vibration.text);

        Setting();
    }

    //IEnumerator SceneLoad()
    //{
    //    var oper = SceneManager.LoadSceneAsync("SampleScene");

    //    yield return new WaitUntil(() => oper.isDone);


    //}

    public void Setting()
    {
        powerSetting.SetActive(!powerSetting.activeSelf);
    }
}
