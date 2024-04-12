using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
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
        Color color;
        SpriteRenderer spriteRenderer;      
        Image image;
        TextMeshProUGUI text;
        fadeAmount = 150f / 250f;

        foreach (GameObject obj in objectsToFade){
            spriteRenderer = obj.GetComponent<SpriteRenderer>();

            if (spriteRenderer != null)
            {
                color = spriteRenderer.color;
                color.a = fadeAmount;
                spriteRenderer.color = color;
                continue;
            }

            image = obj.GetComponent<Image>();
            if (image != null)
            {       
                color = image.color;
                color.a = fadeAmount;
                image.color = color;
                continue;
            }

            text = obj.GetComponent<TextMeshProUGUI>();
            if (text != null)
            {
                color = text.color;
                color.a = fadeAmount;
                text.color = color;
                continue;
            }
        }

        shop.gameObject.SetActive(true);
    }
}
