using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class S_BossChargeBarController : MonoBehaviour
{
    private Slider _slider;
    private int _value;
    [SerializeField] private int _chance;
    void Start()
    {
        _slider = GetComponent<Slider>();
        _slider.maxValue = 200;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        int rand = Random.Range(0, 100);
        if(rand <= _chance && S_TCP_Client._TCP_Instance.Connected)
        {
            _value++;
            _slider.value = _value;
        }
    }
}
