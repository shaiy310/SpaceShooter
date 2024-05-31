using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance { get; private set; }

    // Variables
    [SerializeField] List<Material> weaponMaterials = new List<Material>();
    [SerializeField] List<Material> bodyMaterials = new List<Material>();
    [SerializeField] Renderer weaponPreview;
    [SerializeField] Renderer[] bodyPreview;
    [SerializeField] Renderer materialWeaponPreview;
    [SerializeField] Renderer materialBodyPreview;

    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject inventory;

    public static int WeaponIndex = 0;
    public static int bodyIndex = 0;

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

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
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
        inventory.SetActive(false);
        mainMenu.SetActive(true);
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
