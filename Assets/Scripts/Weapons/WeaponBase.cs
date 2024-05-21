using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Weapons/Create new weapon")]
public class WeaponBase : ScriptableObject
{
    [SerializeField] string weaponName;
    [SerializeField] GameObject gunPrefab;
    [SerializeField] AmmoBase ammo;
    public WeaponStats weaponStats;
    
    public AmmoBase Ammo => ammo;

    public GameObject GunPrefab => gunPrefab;
}
