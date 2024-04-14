using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Weapons/Create new weapon")]
public class WeaponBase : ScriptableObject
{
    [SerializeField] GameObject gunPrefab;
    [SerializeField] AmmoBase ammo;


    public AmmoBase Ammo => ammo;
}
