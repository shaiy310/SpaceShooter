using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    //Variables
    [SerializeField] WeaponBase[] weapons;
    [SerializeField] RenderTexture renderTexture;
    [SerializeField] Camera weaponCamera;
    private GameObject currentWeaponInstance;
    private int selectedOption;

    public int weaponsCount
    {
        get { return weapons.Length; }
    }

    public WeaponBase GetCurrentWeapon()
    {
        return weapons[selectedOption];
    }

    public void NextOption()
    {
        selectedOption++;

        if (selectedOption >= weaponsCount)
        {
            selectedOption = 0;
        }

        // Upate the next weapon
    }

    public void PrevOption()
    {
        selectedOption--;

        if (selectedOption <= 0)
        {
            selectedOption = weapons.Length - 1;
        }

        // Upate the next weapon
    }

    private void UpdateWeapon()
    {
        if (currentWeaponInstance != null)
        {
            Destroy(currentWeaponInstance);
        }

        // Show the current weapon
        WeaponBase currentWeapon = GetCurrentWeapon();
        currentWeaponInstance = Instantiate(currentWeapon.GunPrefab);
        currentWeaponInstance.transform.position = weaponCamera.transform.position + weaponCamera.transform.forward * 2f;
        weaponCamera.targetTexture = renderTexture;
    }
}
