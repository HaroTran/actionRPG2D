using UnityEngine;
using System.Collections.Generic;

public class EnemyHealth : DamageReceiver
{
    [SerializeField] private EnemyCtrl enemyCtrl;
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int currentHealth;
    public int CurrentHealth => currentHealth;

    protected override void ResetAllComponents()
    {
        base.ResetAllComponents();
        if (enemyCtrl == null)
        {
            enemyCtrl = GetComponentInParent<EnemyCtrl>();
        }
        maxHealth = enemyCtrl.EnemyStatsSO.MaxHealth;
        currentHealth = maxHealth;
    }

    protected override void Awake()
    {
        base.Awake();
        ResetAllComponents();
    }

    public void TakeDamage(int damage, Transform attacker = null)
    {
        enemyCtrl.ObjectEffect.PlayEffects(
            new List<EffectName>() { EffectName.Knockback,EffectName.SpriteFlash }
            , attacker);
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    private void Die()
    {
        Debug.Log("Enemy Died");
        // Implement enemy death logic here
    }
}
