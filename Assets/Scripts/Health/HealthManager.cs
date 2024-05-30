using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public static HealthManager instance;

    [SerializeField] TextMeshProUGUI healthText;
    [SerializeField] Image healthBar;
    
    public float HealthAmount {  get; private set; }

    void Start()
    {
        instance = this;
        HealthAmount = 100f;
        FillAmount();
    }

    public void TakeDamage(float damage)
    {
        HealthAmount = Mathf.Clamp(HealthAmount - damage, 0, 100);
        if (HealthAmount == 0) {
            StartCoroutine(PlayerMovement.instance.Respawn());
            HealthAmount = 100;
        } else {
            StartCoroutine(Fadder.instance.FadeAnimation(Color.red, 0.2f, 0.75f));
        }

        FillAmount();
    }

    public void Heal(float healingAmount)
    {
        HealthAmount = Mathf.Clamp(HealthAmount + healingAmount, 0, 100);
        FillAmount();
    }

    void FillAmount()
    {
        healthBar.fillAmount = HealthAmount / 100;
        healthText.text = $"{HealthAmount}%";
    }
}
