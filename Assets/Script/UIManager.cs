using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject OptionPopup;
    public GameObject LobbyUI;
    public GameObject InGameUI;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
