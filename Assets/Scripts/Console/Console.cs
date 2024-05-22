using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Console : MonoBehaviour, IInteractable
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
        screen.PostMessage("shield up");
    }

    public void UpdateAmmo(PlayerRayCast shooter)
    {
        screen.PostMessage("new ammo");
        shooter.SwitchWeapon();
    }

    public void UnlockDoor(RegularDoorEngine door)
    {
        screen.PostMessage("Engineering door unlocked");
        door.enabled = true;
    }
}
