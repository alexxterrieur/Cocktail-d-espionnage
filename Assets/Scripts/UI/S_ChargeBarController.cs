using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class S_ChargeBarController : MonoBehaviour
{
    private Slider _slider; 
    void Start()
    {
        _slider = GetComponent<Slider>();
        _slider.maxValue = 200;
    }

    // Update is called once per frame
    void Update()
    {
        _slider.value = S_TCP_Client._TCP_Instance.JoltScore;
       
        if (_slider.value >= 200)
        {
            Debug.Log("BITE!");
        }
    }
}
