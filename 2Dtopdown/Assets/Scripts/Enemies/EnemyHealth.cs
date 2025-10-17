using UnityEngine;
using System.Collections.Generic;
using System;
using Unity.VisualScripting;

public class EnemyHealth : DamageReceiver
{
    [SerializeField] private EnemyCtrl enemyCtrl;
    [SerializeField] private int maxHealth = 100;
    public int MaxHealth => maxHealth;
    [SerializeField] private int currentHealth;
    public int CurrentHealth => currentHealth;

    public event Action<int, int> OnHealthChanged;

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
    protected void Start()
    {
        OnHealthChanged?.Invoke(currentHealth, maxHealth);
    }

    public void TakeDamage(int damage, Transform attacker = null)
    {
        enemyCtrl.ObjectEffect.PlayEffects(
            new List<EffectName>() { EffectName.Knockback,EffectName.SpriteFlash }
            , attacker);
        currentHealth -= damage;
        OnHealthChanged?.Invoke(currentHealth, maxHealth);
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    private void Die()
    {
        Debug.Log("Enemy Died");
        // Implement enemy death logic here
        Transform soulvfx = VFX_Spawner.Instance.Spawn(VFX_name.Soul, enemyCtrl.transform.position, Quaternion.identity);
        soulvfx.GetComponent<SoulVFXCtrl>().SetSoulValue(enemyCtrl.EnemyStatsSO.SoulDropValue);
        enemyCtrl.gameObject.SetActive(false);
    }
}
