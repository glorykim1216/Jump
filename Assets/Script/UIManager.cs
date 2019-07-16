using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Lofle.Tween;
public class UIManager : MonoBehaviour
{
    public GameObject OptionPopup;
    public GameObject LobbyUI;
    public GameObject InGameUI;
    public GameObject ScrollView;
    public TweenPosition tween;
    // Start is called before the first frame update
    void Start()
    {
       
      
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            if(tween.isForward)
                tween.PlayReverse();
            else
                tween.PlayForward();
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
           
        }
    }

    public void SkinButton()
    {
        if (tween.isForward)
        {
            tween.PlayReverse();
        }
          
        else
        {
            tween.PlayForward();
        }
           
    }

    public void SkinSelectedValue()
    {
        Debug.Log("작동 잘됨");
    }

    public void OpenOption()
    {
        OptionPopup.SetActive(true);
    }

    public void CloseOption()
    {
        OptionPopup.SetActive(false);
    }

    public void StartGame()
    {
        LobbyUI.SetActive(false);
        InGameUI.SetActive(true);
    }

    public void OpenSkinView()
    {
        ScrollView.SetActive(true);
    }
    public void CloseSkinView()
    {
        ScrollView.SetActive(false);
    }
}
