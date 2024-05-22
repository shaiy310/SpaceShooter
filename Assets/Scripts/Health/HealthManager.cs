using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI healthText;
    [SerializeField] Image healthBar;
    public static float healthAmount;

    void Start()
    {
        healthAmount = 100f;
        healthText.text = healthAmount.ToString() + "%";
    }

    private void Update()
    {
        FillAmount();

        if (healthAmount <= 0)
            healthAmount = 0;

        healthText.text = healthAmount.ToString() + "%";
    }

    public void takeDamage(float damage)
    {
        healthAmount -= damage;
        FillAmount();
    }

    public void Heal(float healingAmount)
    {
        healthAmount += healingAmount;
        healthAmount = Mathf.Clamp(healthAmount, 0, 100);
        FillAmount();
    }

    void FillAmount()
    {
        healthBar.fillAmount = healthAmount / 100;
    }
}
