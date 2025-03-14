using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthAndShield : MonoBehaviour
{
    public Slider healthSlider;
    public Slider shieldSlider;

    public int maxHealth = 10;
    public int currentHealth;

    public int maxShield = 5;
    public int currentShield;

    private void Start()
    {
        currentHealth = maxHealth;
        currentShield = maxShield;

        healthSlider.maxValue = maxHealth;
        shieldSlider.maxValue = maxShield;

        UpdateUI();
    }

    public void TakeDamage(int damage)
    {
        if (currentShield > 0)
        {
            currentShield -= damage;
            if (currentShield < 0)
            {
                currentHealth += currentShield; // Remaining damage goes to health
                currentShield = 0;
            }
        }
        else
        {
            currentHealth -= damage;
        }

        if (currentHealth <= 0)
        {
            Destroy(gameObject); // Player dies
        }

        UpdateUI();
    }

    void UpdateUI()
    {
        healthSlider.value = currentHealth;
        shieldSlider.value = currentShield;
    }
}
