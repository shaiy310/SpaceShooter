using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MMManager : MonoBehaviour
{
    // Variables
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject inventory;
    [SerializeField] GameObject shop;
    [SerializeField] GameObject[] objectsToFade;
    [SerializeField] float fadeAmount;

    public void Play()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void OpenInventory()
    {
        mainMenu.gameObject.SetActive(false);
        inventory.gameObject.SetActive(true);
    }

    public void CloseInventory()
    {
        inventory.gameObject.SetActive(false);
        mainMenu.gameObject.SetActive(true);
    }

    public void OpenShop()
    {
        shop.SetActive(true);
        mainMenu.SetActive(false);
    }
}
