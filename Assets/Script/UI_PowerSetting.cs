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
        jump.text = player.jumpPower.ToString();
        up.text = player.upPower.ToString();
        forward.text = player.forwardPower.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        player.jumpPower = float.Parse(jump.text);
        player.upPower = float.Parse(up.text);
        player.forwardPower = float.Parse(forward.text);
    }

    public void ReStart()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void Setting()
    {
        powerSetting.SetActive(!powerSetting.activeSelf);
    }
}
