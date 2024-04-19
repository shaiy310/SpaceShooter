using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Weapons/Create new ammo")]
public class AmmoBase : ScriptableObject
{
    [SerializeField] GameObject bulletPrefab;

    public GameObject Bullet => bulletPrefab;
}
