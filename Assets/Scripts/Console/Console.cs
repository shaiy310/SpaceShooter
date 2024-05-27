using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Console : MonoBehaviour, IInteractable
{
    [SerializeField] UnityEvent action;

    public void Interact()
    {
        action?.Invoke();
    }

    public void UpdateAmmo(PlayerRayCast shooter)
    {
        shooter.SwitchWeapon();
    }
}
