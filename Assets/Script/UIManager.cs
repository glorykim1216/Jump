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
    public TweenPosition tween;
    public Transform ArrowRot;

    // Start is called before the first frame update
    void Start()
    {
        //tween 시작시 2번클릭 방지
        tween.PlayReverse();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
          
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
           
        }
    }

    public void SkinButton()
    {
        //트윈 위치 설정 (0,0,0,)으로 위치 이동하는 현상 제거
        tween.From = new Vector3(tween.transform.localPosition.x, 300, 0);
        tween.To = new Vector3(tween.transform.localPosition.x, 0, 0);
        if (tween.isForward)
        {
            ArrowRot.localRotation = Quaternion.Euler(new Vector3(0, 0, -50));
            tween.PlayReverse();
        }
         
        else
        {
            ArrowRot.localRotation = Quaternion.Euler(new Vector3(0, 0, 130));
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

}
