using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class WeaponStats
{
    [SerializeField] float damage;

    public float Damage { get { return damage; } set {  damage = value; } }
}
