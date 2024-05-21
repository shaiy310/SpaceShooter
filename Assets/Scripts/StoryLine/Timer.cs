using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    public static float remainingTime;
    private float minute;
    private float firstPhaseMinutes;
   
    void Start()
    {
        minute = 60f;
        firstPhaseMinutes = 0.5f;
        remainingTime = firstPhaseMinutes * minute;
    }

    void Update()
    {
        firstPhaseTimer();
    }

    void firstPhaseTimer()
    {
        if (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
        }
        else if (remainingTime < 0)
        {
            remainingTime = 0;
            timerText.color = Color.red;               
        }
        
        int minutes = Mathf.FloorToInt(remainingTime / minute);
        int seconds = Mathf.FloorToInt(remainingTime % minute);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
