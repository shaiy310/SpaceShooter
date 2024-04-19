using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class WeaponStats
{
    [SerializeField] float damage;
    [SerializeField] float clipSize;
    [SerializeField] float accuracy;
    [SerializeField] float range;
    [SerializeField] bool isAuto;

    public float Damage { get { return damage; } set {  damage = value; } }
    public float ClipSize { get { return clipSize; } set { clipSize = value; } }
    public float Accuracy { get { return accuracy; } set { accuracy = value; } }
    public float Range { get { return range; } set { range = value; } }
    public bool IsAuto => isAuto;
}
