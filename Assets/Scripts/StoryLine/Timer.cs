using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public static Timer Instance { get; private set; }

    [SerializeField] TextMeshProUGUI timerText;

    public static float remainingTime;
    private float minute;
    private float firstPhaseMinutes;

    private void Awake()
    {
        Instance = this;
        minute = 60f;
        firstPhaseMinutes = 0.5f;
        remainingTime = firstPhaseMinutes * minute;
    }

    void Start()
    {
        StartCoroutine(RunTimer());
    }

    IEnumerator RunTimer()
    {
        while (true)
        {
            if (remainingTime > 0)
            {
                remainingTime--;
            }
            else if (remainingTime < 0)
            {
                remainingTime = 0;
                timerText.color = Color.red;      
            }

            int minutes = Mathf.FloorToInt(remainingTime / minute);
            int seconds = Mathf.FloorToInt(remainingTime % minute);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

            yield return new WaitForSeconds(1f);
        }
    }

    public void StopTimer()
    {
        StopCoroutine(RunTimer());
        timerText.enabled = false;
    }
}
