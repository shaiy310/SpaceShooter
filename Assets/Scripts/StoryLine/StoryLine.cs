using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryLine : MonoBehaviour
{
    [SerializeField] Image fadedScreen;

    private float decreaseDmgMinute;
    private float damageDecreasePerSec;

    private void Start()
    {
        StartCoroutine(InitializeAfterTimer());
        StartCoroutine(DealDamage());
    }

    private IEnumerator InitializeAfterTimer()
    {
        yield return new WaitUntil(() => Timer.remainingTime > 0);

        decreaseDmgMinute = Timer.remainingTime / 10;
        damageDecreasePerSec = Mathf.Round((HealthManager.healthAmount / decreaseDmgMinute) * 10) / 10f + 0.1f;
    }

    private IEnumerator DealDamage()
    {
        yield return new WaitUntil(() => Timer.remainingTime <= decreaseDmgMinute && Timer.remainingTime > 0);

        StartCoroutine(DecreasePlayerHealth());
        StartCoroutine(FadedScreen());
    }

    // If hp got to 0:
    // Active die animation
    // Stop the game
    // Show the player a screen 

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
        var color = fadedScreen.color;
        color.a = 0.8f;
        fadedScreen.color = color;


        while (Timer.remainingTime > 0)
        {
            while (color.a > 0)
            {
                color.a -= Time.deltaTime / 2;
                fadedScreen.color = color;
                yield return null;
            }

            color.a = 0;
            fadedScreen.color = color;

            yield return new WaitForSeconds(0.1f);

            while (color.a < 0.8f)
            {
                color.a += Time.deltaTime / 2;
                fadedScreen.color = color;
                yield return null;
            }
        }
    }
}
