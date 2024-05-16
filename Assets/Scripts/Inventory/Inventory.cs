using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    //Variables
    [SerializeField] WeaponBase[] weapons;
    [SerializeField] RenderTexture renderTexture;
    [SerializeField] Camera weaponCamera;
    [SerializeField] TextMeshProUGUI weaponName;
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject inventory;
    [SerializeField] GameObject upgrade;
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

        UpdateWeapon();
    }

    public void PrevOption()
    {
        selectedOption--;

        if (selectedOption <= 0)
        {
            selectedOption = weapons.Length - 1;
        }

        UpdateWeapon();
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
        weaponName.text = currentWeapon.name;
    }

    public void Back()
    {
        inventory.gameObject.SetActive(false);
        mainMenu.gameObject.SetActive(true);
    }

    public void Upgrade()
    {
        // TODO: create the upgrade window
        inventory.gameObject.SetActive(false);
        upgrade.gameObject.SetActive(true);
    }
}
