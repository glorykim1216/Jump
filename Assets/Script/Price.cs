using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Price : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        this.GetComponentInChildren<Text>().text = UIManager.Instance.priceTemp.ToString();
        this.GetComponentInChildren<RawImage>().texture = UIManager.Instance.renderTemp;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
