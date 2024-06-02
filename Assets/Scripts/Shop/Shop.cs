using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Shop : MonoBehaviour
{
    //Variables
    [SerializeField] Material[] materials;
    [SerializeField] Renderer weaponPreview;
    [SerializeField] Renderer[] bodyPreview;
    [SerializeField] Renderer materialWeaponPreview;
    [SerializeField] Renderer materialBodyPreview;

    [SerializeField] TextMeshProUGUI coin;
    [SerializeField] TextMeshProUGUI coinToBuyWeapon;
    [SerializeField] TextMeshProUGUI coinToBuyBody;

    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject shop;

    [SerializeField] Inventory newMaterials;

    private int WeaponIndex;
    private int bodyIndex;

    private void Start()
    {
        WeaponIndex = 0;
        bodyIndex = 0;

        coin.text = Coins.mainCoins.ToString();
        coinToBuyWeapon.text = Coins.shopCoins.ToString();
        coinToBuyBody.text = Coins.shopCoins.ToString();

        weaponPreview.material = materials[WeaponIndex];
        materialWeaponPreview.material = materials[WeaponIndex];

        materialBodyPreview.material = materials[bodyIndex];

        foreach (var part in bodyPreview)
        {
            part.material = materials[bodyIndex];
        }

        newMaterials = FindObjectOfType<Inventory>();
    }

    public void Back()
    {
        shop.SetActive(false);
        mainMenu.SetActive(true);
    }

    public int NextOption(int currentIndex)
    {
        currentIndex++;

        if (currentIndex >= materials.Length)
        {
            currentIndex = 0;
        }

        return currentIndex;
    }

    public int PrevOption(int currentIndex)
    {
        currentIndex--;

        if (currentIndex < 0)
        {
            currentIndex = materials.Length - 1;
        }

        return currentIndex;
    }

    public void UpdateWeaponMaterial(string direction)
    {
        if (direction == "Right")
        {
            WeaponIndex = NextOption(WeaponIndex);
        }
        else
        {
            WeaponIndex = PrevOption(WeaponIndex);
        }

        weaponPreview.material = materials[WeaponIndex];
        materialWeaponPreview.material = materials[WeaponIndex];
    }

    public void UpdateBodyMaterial(string direction)
    {
        if (direction == "Right")
        {
            bodyIndex = NextOption(bodyIndex);
        }
        else
        {
            bodyIndex = PrevOption(bodyIndex);
        }

        materialBodyPreview.material = materials[bodyIndex];

        foreach (var part in bodyPreview)
        {
            part.material = materials[bodyIndex];
        }
    }

    public void BuyMaterial(string part)
    {
        Coins.mainCoins -= Coins.shopCoins;
        coin.text = Coins.mainCoins.ToString();

        AddMaterialToInventory(part);
    }

    public void AddMaterialToInventory(string part)
    {
        if (part == "Weapon")
        {
            newMaterials.WeaponMaterials.Add(materials[WeaponIndex]);
        }
        else
        {
            newMaterials.BodyMaterials.Add(materials[bodyIndex]);
        }
        
    }
}

