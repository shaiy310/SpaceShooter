using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PopUpScreen : MonoBehaviour
{
    public static PopUpScreen Instance { get; private set; }

    [SerializeField] TextMeshProUGUI popUpMassege;
    [SerializeField] TextMeshProUGUI popUpTitle;

    private void Awake()
    {        
        Instance = this;
        gameObject.SetActive(false);
    }

    public void ShowPopUpScreen(string popUpText)
    {
        gameObject.SetActive(true);
        Cursor.visible = true;
        popUpMassege.text = popUpText;
        popUpTitle.text = "PHASE ONE";
    }

    public void cancelPopUp()
    {
        gameObject.SetActive(false);
        Cursor.visible = false;
    }
}
