using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Clock : MonoBehaviour
{
    public bool onPause = false;

    private const float realSecondsPerIngameDay = 600f;        //Allow to change the real seconds (in real life) as in game day time
    private Transform clockHourHandTransform;
    private Transform clockMinuteHandTransform;
    private TextMeshProUGUI timeText;
    private float day = 0.625f;                                 //Allows to initiate the hours of the clock (exemple: 0.5f = 12:00)

    private void Awake()
    {
        clockHourHandTransform = transform.Find("HourHand");
        clockMinuteHandTransform = transform.Find("MinuteHand");
        timeText = transform.Find("TimeText").GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        if (!onPause)
        {
            day += Time.deltaTime / realSecondsPerIngameDay;

            float dayNormalized = day % 1f;
            float rotationDegreesPerDay = 360f;
            clockHourHandTransform.eulerAngles = new Vector3(0, 0, -dayNormalized * rotationDegreesPerDay * 2);

            float hoursPerDay = 24f;
            clockMinuteHandTransform.eulerAngles = new Vector3(0, 0, -dayNormalized * rotationDegreesPerDay * hoursPerDay);

            string hoursString = Mathf.Floor(dayNormalized * hoursPerDay).ToString("00");
            float minutesPerHour = 60f;
            string minutesString = Mathf.Floor(((dayNormalized * hoursPerDay) % 1f) * minutesPerHour).ToString("00");

            timeText.text = hoursString + ":" + minutesString;

            TimerIsOver();
        }
    }

    private void TimerIsOver()
    {
        if (day >= 1f)
        {
            SceneManager.LoadScene("LukaTestScene");
        }
    }

    public void SetPause(bool pause)
    {
        onPause = pause;
    }
}