using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    public Image healthBarFill;          // Assign this in the Inspector
    public PlayerHealth player;    // Reference to the script holding PlayerHealth

    private float maxHealth;

    void Start()
    {
        if (player != null)
        {
            maxHealth = player.maxHealth;  // Get starting health
        }
    }

    void Update()
    {
        if (player != null)
        {
            float currentHealth = player.maxHealth;
            float fillAmount = currentHealth / maxHealth;
            healthBarFill.fillAmount = Mathf.Clamp01(fillAmount); // Ensure value stays between 0 and 1
        }
    }
}
