using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ConsoleScreen : MonoBehaviour, IInteractable
{
    [SerializeField] ScreenMessage screen;
    [SerializeField] UnityEvent action;

    //[SerializeField] 


    public void Interact()
    {
        action?.Invoke();
    }

    public void IncreaseShield()
    {

    }

    public void UpdateAmmo(PlayerRayCast shooter)
    {

    }

    public void OpenDoor()
    { }
}
