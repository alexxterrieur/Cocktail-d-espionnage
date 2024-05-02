using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class S_ChargeBarController : MonoBehaviour
{
    private Slider _slider;
    [SerializeField] private Slider _bossSlider;
    private int _value;
    [SerializeField] private int _chance;


    void Start()
    {
        _slider = GetComponent<Slider>();
        _slider.maxValue = 200;
        _bossSlider.maxValue = 200;
    }

    // Update is called once per frame
    void Update()
    {
        _slider.value = S_TCP_Client._TCP_Instance.JoltScore;
       
        if (_slider.value >= 200)
        {
            
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        int rand = Random.Range(0, 100);
        if (rand <= _chance && S_TCP_Client._TCP_Instance.Connected)
        {
            _value++;
            _bossSlider.value = _value;
        }
    }
}
