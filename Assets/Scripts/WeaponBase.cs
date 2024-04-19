using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Weapons/Create new weapon")]
public class WeaponBase : ScriptableObject
{
    [SerializeField] GameObject gunPrefab;
    [SerializeField] AmmoBase ammo;
    [SerializeField] WeaponStats weapon;
    
    public AmmoBase Ammo => ammo;

    public GameObject GunPrefab => gunPrefab;
}
