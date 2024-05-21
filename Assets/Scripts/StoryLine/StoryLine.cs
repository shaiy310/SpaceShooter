using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryLine : MonoBehaviour
{
    [SerializeField] GameObject fadedScreen;

    private bool isCoroutineRunning;
    private float decreaseDmgMinute;
    private float damageDecreasePerSec;

    private void Start()
    {
        StartCoroutine(InitializeAfterTimer());
    }

    private IEnumerator InitializeAfterTimer()
    {
        yield return new WaitUntil(() => Timer.remainingTime > 0);

        decreaseDmgMinute = Timer.remainingTime / 10;
        isCoroutineRunning = false;
        damageDecreasePerSec = Mathf.Round((HealthManager.healthAmount / decreaseDmgMinute) * 10) / 10f + 0.1f;
    }

    void Update()
    {
        if (!isCoroutineRunning && Timer.remainingTime <= decreaseDmgMinute && Timer.remainingTime > 0)
        {
            StartCoroutine(DecreasePlayerHealth());
            StartCoroutine(FadedScreen());
            isCoroutineRunning = true;
        }

        // If hp got to 0:
        // Active die animation
        // Stop the game
        // Show the player a screen 
    }

    IEnumerator DecreasePlayerHealth()
    {
        while (Timer.remainingTime > 0)
        {
            HealthManager.healthAmount -= damageDecreasePerSec;
            yield return new WaitForSeconds(1f);
        }
    }

    IEnumerator FadedScreen()
    {
        var color = fadedScreen.GetComponent<Image>().color;
        color.a = 0.8f;
        fadedScreen.GetComponent<Image>().color = color;

        while (Timer.remainingTime > 0)
        {
            while (color.a > 0)
            {
                color.a -= Time.deltaTime / 2;
                fadedScreen.GetComponent<Image>().color = color;
                yield return null;
            }

            color.a = 0;
            fadedScreen.GetComponent<Image>().color = color;

            yield return new WaitForSeconds(0.1f);

            color.a = 0.8f;
            fadedScreen.GetComponent<Image>().color = color;
        }
    }
}
