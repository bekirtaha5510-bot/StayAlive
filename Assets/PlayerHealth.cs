using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public Slider healthBar;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.maxValue = maxHealth;
        healthBar.value = currentHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.value = currentHealth;

        Debug.Log("Player caný: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("PLAYER ÖLDÜ!");

        GameOverManager gom = FindObjectOfType<GameOverManager>();
        if (gom != null)
            gom.ShowGameOver();
        else
            Time.timeScale = 0f; // olmazsa yine dursun
    }

}
