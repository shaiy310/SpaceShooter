using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    //Variables
    [SerializeField] List<Material> weaponMaterials = new List<Material>();
    [SerializeField] List<Material> bodyMaterials = new List<Material>();
    [SerializeField] Renderer weaponPreview;
    [SerializeField] Renderer[] bodyPreview;
    [SerializeField] Renderer materialWeaponPreview;
    [SerializeField] Renderer materialBodyPreview;

    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject inventory;

    public static int WeaponIndex;
    public static int bodyIndex;

    public List<Material> WeaponMaterials
    {
        get { return weaponMaterials; }
        set { weaponMaterials = value; }
    }

    public List<Material> BodyMaterials
    {
        get { return bodyMaterials; }
        set { bodyMaterials = value; }
    }

    private void Start()
    {
        WeaponIndex = 0;
        bodyIndex = 0;

        weaponPreview.material = weaponMaterials[WeaponIndex];
        materialWeaponPreview.material = weaponMaterials[WeaponIndex];

        materialBodyPreview.material = BodyMaterials[bodyIndex];
        foreach (var part in bodyPreview)
        {
            part.material = BodyMaterials[bodyIndex];
        }
    }

    public void Back()
    {
        inventory.gameObject.SetActive(false);
        mainMenu.gameObject.SetActive(true);
    }

    public int NextOption(int currentIndex, int maxCount)
    {
        currentIndex++;

        if (currentIndex >= maxCount)
        {
            currentIndex = 0;
        }

        return currentIndex;
    }   

    public int PrevOption(int currentIndex, int maxCount)
    {
        currentIndex--;

        if (currentIndex < 0)
        {
            currentIndex = maxCount - 1;
        }

        return currentIndex;
    }

    public void UpdateWeaponMaterial(string direction)
    {
        if (direction == "Right")
        {
            WeaponIndex = NextOption(WeaponIndex, weaponMaterials.Count);           
        }
        else
        {
            WeaponIndex = PrevOption(WeaponIndex, weaponMaterials.Count);
        }

        weaponPreview.material = weaponMaterials[WeaponIndex];
        materialWeaponPreview.material = weaponMaterials[WeaponIndex];
    }

    public void UpdateBodyMaterial(string direction)
    {
        if (direction == "Right")
        {
            bodyIndex = NextOption(bodyIndex, bodyMaterials.Count);
        }
        else
        {
            bodyIndex = PrevOption(bodyIndex, bodyMaterials.Count);
        }

        materialBodyPreview.material = BodyMaterials[bodyIndex];

        foreach (var part in bodyPreview)
        {
            part.material = BodyMaterials[bodyIndex];
        }
    }
}
