using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI_PowerSetting : MonoBehaviour
{
    public player player;
    public GameObject powerSetting;
    public InputField jump;
    public InputField up;
    public InputField forward;
    // Start is called before the first frame update
    void Start()
    {
        


        player.upValue = StaticData.jumpPower;
        player.upPower = StaticData.upPower;
        player.forwardPower = StaticData.forwardPower;

        jump.text = player.upValue.ToString();
        up.text = player.upPower.ToString();
        forward.text = player.forwardPower.ToString();
    }

    // Update is called once per frame
    void Update()
    {
  
    }

    public void ReStart()
    {
        //SceneManager.LoadScene("SampleScene");
        StaticData.jumpPower = float.Parse(jump.text);
        StaticData.upPower = float.Parse(up.text);
        StaticData.forwardPower = float.Parse(forward.text);
        StartCoroutine(SceneLoad());
       
    }

    IEnumerator SceneLoad()
    {
        var oper = SceneManager.LoadSceneAsync("SampleScene");

        yield return new WaitUntil(() => oper.isDone);


    }

    public void Setting()
    {
        powerSetting.SetActive(!powerSetting.activeSelf);
    }
}
