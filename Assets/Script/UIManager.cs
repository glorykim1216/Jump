using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    public GameObject OptionPopup;
    public GameObject LobbyUI;
    public GameObject InGameUI;
    public GameObject ScrollView;
    Dropdown m_Dropdown;
    List<string> m_DropOptions;
    int _iSkinNum=1;
    // Start is called before the first frame update
    void Start()
    {
        m_Dropdown = ScrollView.GetComponent<Dropdown>();
        m_DropOptions = new List<string> { "Skin 1", "Skin 2", "Skin 3", "Skin 4", "Skin 5" };
        m_Dropdown.ClearOptions();
      
        m_Dropdown.AddOptions(m_DropOptions);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            //스킨획득시 이코드 실행(해당 리스트는 DB로 관리되어야됨)
            //_iSkinNum++;
            //m_DropOptions.Add("Skin " + _iSkinNum);
            
            //로비화면 보일때

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
