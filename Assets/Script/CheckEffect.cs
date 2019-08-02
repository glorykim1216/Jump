using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CheckEffect : MonoBehaviour
{
    public bool BuyCheck;
    public int needMoney;
    public Text moneyText;
    public Image moneyImg;
    public GameObject CheckImg;
    public int SkinNumber;
    public bool _bBuyCheck
    {
        get { return BuyCheck; }
        set
        {
            BuyCheck = value;

        }
    }
    // Start is called before the first frame update
    void Awake()
    {
        moneyText = this.transform.parent.GetComponentInChildren<Text>();
        moneyImg = this.transform.parent.Find("MoneyIMG").GetComponent<Image>();
        CheckImg = this.transform.parent.Find("CheckIMG").gameObject;
        moneyText.text = needMoney.ToString();


        if (BuyCheck)
        {

        }
    }
}
