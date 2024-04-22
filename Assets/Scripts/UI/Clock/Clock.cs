using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Clock : MonoBehaviour
{
    public bool onPause = false;

    private const float realSecondsPerIngameDay = 86400f;       //Allow to change the real seconds (in real life) as in game day time
    private Transform clockHourHandTransform;
    private Transform clockMinuteHandTransform;
    private TextMeshProUGUI timeText;
    private float day = 0f;                                 //Allows to initiate the hours of the clock (exemple: 0.5f = 12:00)
    private float remainingSeconds = 600f;

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
            remainingSeconds -= Time.deltaTime;

            float dayNormalized = day % 1f;
            float rotationDegreesPerDay = 360f;
            clockHourHandTransform.eulerAngles = new Vector3(0, 0, -dayNormalized * rotationDegreesPerDay * 2f);                                                //Rotation HourClock hand ( *2 to make a 12h clock)

            float hoursPerDay = 24f;
            clockMinuteHandTransform.eulerAngles = new Vector3(0, 0, -dayNormalized * rotationDegreesPerDay * hoursPerDay);                                     //Rotation MinuteClock hand

            float minutesPerHour = 60f;
            string minutesString = Mathf.Floor(remainingSeconds / minutesPerHour).ToString("00");                                                               //Display the minutes left of the timer
            float secondsPerMinutes = 60f;
            string secondsString = Mathf.Floor(remainingSeconds % secondsPerMinutes).ToString("00");                                                            //Display the seconds left of the timer

            timeText.text = minutesString + ":" + secondsString;

            TimerIsOver();
        }
    }

    private void TimerIsOver()
    {
        if (remainingSeconds <= 0f)
        {
            SceneManager.LoadScene("LukaTestScene");
        }
    }

    public void SetPause(bool pause)
    {
        onPause = pause;
    }
}