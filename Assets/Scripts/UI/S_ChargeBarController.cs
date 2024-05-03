using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
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
        if (_bossSlider.value >= 200)
        {
            Load(S_GameOverManager.GameOver.FinalFight);
        }
        else if (_slider.value >= 200 && S_SaveDataExternal.JournalData.Proofs.Length != 5)
        {
            Load(S_GameOverManager.GameOver.WinButLose);
        }
        else
        {
            Load(S_GameOverManager.GameOver.Win);
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

    IEnumerator Load(S_GameOverManager.GameOver lose)
    {
        yield return new WaitForSecondsRealtime(5);

        S_GameOverManager.Instance.GameOverType = lose;
        SceneManager.LoadScene("GameOver");
    }

}
