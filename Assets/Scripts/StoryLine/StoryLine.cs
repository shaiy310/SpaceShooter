using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StoryLine : MonoBehaviour
{
    public static StoryLine Instance { get; private set; }

    [SerializeField] Image fadedScreen;
    [SerializeField] GameObject exitButton;
    [SerializeField] GameObject retryButton;

    private float decreaseDmgMinute;
    private float damageDecreasePerSec;

    private void Start()
    {
		Instance = this;
		
        PopUpScreen.Instance.ShowPopUpScreen("Active the space station air machine within the limited time");


        decreaseDmgMinute = Timer.remainingTime / 10;
        damageDecreasePerSec = Mathf.Round((HealthManager.Instance.HealthAmount / decreaseDmgMinute) * 10) / 10f + 0.1f;
        
		StartCoroutine(DealDamage());
    }

    public void CompleteStoryLine()
    {
        StopCoroutine(DealDamage());
        PopUpScreen.Instance.ShowPopUpScreen("PHASE ONE COMPLETED! YOU GOT 10 COINS!");
        exitButton.SetActive(true);
        Coins.mainCoins += 10;
    }

    private IEnumerator DealDamage()
    {
        yield return new WaitUntil(() => Timer.remainingTime <= decreaseDmgMinute && Timer.remainingTime > 0);

        StartCoroutine(DecreasePlayerHealth());
        StartCoroutine(FadedScreen());
    }

    public void PlayerDead()
    {
        StopCoroutine(DealDamage());
        // Active die animation
        // Make ienumarator, wait until the animation is end
        PopUpScreen.Instance.ShowPopUpScreen("You are dead, What would you like to do?");

        exitButton.SetActive(true);
        retryButton.SetActive(true);
    }

    public void RetryAgain()
    {
        StartCoroutine(PlayerMovement.Instance.Respawn());
        PopUpScreen.Instance.CancelPopUp();
        exitButton.SetActive(false);
        retryButton.SetActive(false);
    }

    IEnumerator DecreasePlayerHealth()
    {
        while (Timer.remainingTime > 0)
        {
            HealthManager.Instance.TakeDamage(damageDecreasePerSec);
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

    private void Update()
    {
        PauseMenu();
    }

    void PauseMenu()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PopUpScreen.Instance.ShowPopUpScreen("What would you like to do?");
            exitButton.SetActive(true);
        }
    }

    public void ExitGame()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
