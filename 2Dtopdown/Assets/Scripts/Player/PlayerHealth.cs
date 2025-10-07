using UnityEngine;

public class PlayerHealth : DamageReceiver
{
    [SerializeField] private PlayerCtrl playerCtrl;
    public PlayerCtrl PlayerCtrl { get { return playerCtrl; } }
    [SerializeField] private int maxHealth;
    [SerializeField] private int currentHealth;

    public int CurrentHealth { get { return currentHealth; } }

    protected override void ResetAllComponents()
    {
        base.ResetAllComponents();
        if (playerCtrl == null)
        {
            playerCtrl = GetComponentInParent<PlayerCtrl>();
        }
        maxHealth =playerCtrl.PlayerStatsSO.MaxHealth;
        currentHealth = maxHealth;
    }

    protected override void Awake()
    {
        base.Awake();
        ResetAllComponents();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Player Died");
        // Implement player death logic here
    }
}
