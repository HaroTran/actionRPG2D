using UnityEngine;

public class HeathStatsManager : MonoBehaviour
{
    [SerializeField] HeathSO heathSO;
    int currentHealth;

    private void Awake()
    {
        currentHealth = heathSO.MaxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Debug.Log("Player Died");
            // Handle player death (e.g., respawn, game over, etc.)
        }
    }

}
