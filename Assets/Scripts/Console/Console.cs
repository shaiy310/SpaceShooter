using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Console : MonoBehaviour, IInteractable
{
    [SerializeField] ScreenMessage screen;
    [SerializeField] UnityEvent action;

    public void Interact()
    {
        action?.Invoke();
    }

    public void IncreaseShield()
    {
        screen.PostMessage("shield up");
    }

    public void UpdateAmmo(WeaponBase weaponBase)
    {
        screen.PostMessage("new ammo");
        PlayerRayCast.instance.SwitchWeapon(weaponBase);
    }

    public void UnlockDoor(RegularDoorEngine door)
    {
        screen.PostMessage("Engineering door unlocked");
        door.enabled = true;
    }
}
