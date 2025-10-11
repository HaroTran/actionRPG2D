using System.Collections;
using UnityEngine;

public class PlayerAttack : DamageSender
{
    [SerializeField] private PlayerCtrl playerCtrl;
    [SerializeField] private LayerMask targetLayer;
    [SerializeField] private float attackRange = 1f;

    [SerializeField] private float attackCooldown = 1f;

    [SerializeField] private bool isAttacking = false;
    public bool IsAttacking { get { return isAttacking; } }
    [SerializeField] private bool isCanAttack = true;
    public bool IsCanAttack { get { return isCanAttack; } }
    protected override void ResetAllComponents()
    {
        base.ResetAllComponents();
        if (playerCtrl == null)
        {
            playerCtrl = GetComponentInParent<PlayerCtrl>();
        }
        if (playerCtrl != null && playerCtrl.PlayerStatsSO != null)
        {
            attackRange = playerCtrl.PlayerStatsSO.AttackRange;
            attackCooldown = playerCtrl.PlayerStatsSO.AttackCooldown;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J) && isCanAttack)
        {
            //Debug.Log("Player attack");
            isAttacking = true;
            playerCtrl.PlayerStateMachine.ChangeState(playerCtrl.PlayerStateMachine.playerAttackState);
        }
    }

    public void ApplyPlayerAttack()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, attackRange, targetLayer);
        if (hits.Length == 0)
        {
            Debug.Log("No target in range to attack.");
            return;
        }

        foreach (Collider2D potential in hits)
        {
            if (!potential.CompareTag("Enemy"))
            {
                //Debug.Log($"Ignored non-enemy collider: {potential.name}");
                continue;
            }

            //Debug.Log($"Player attack hit: {potential.name}");
            EnemyCtrl enemy = potential.GetComponent<EnemyCtrl>();
            if (enemy != null && enemy.EnemyHealth != null)
            {
                int damage = playerCtrl.PlayerStatsSO.AttackDamage;
                enemy.EnemyHealth.TakeDamage(damage, playerCtrl.transform);
                //Debug.Log($"Enemy took {damage} damage from player.");
            }
        }
    }
    public void StopAttacking()
    {
        isAttacking = false;
        StartCoroutine(CooldownAttack());
    }
    IEnumerator CooldownAttack()
    {
        //Debug.Log("Start Attack cooldown");
        isCanAttack = false;
        yield return new WaitForSeconds(attackCooldown);
        isCanAttack = true;
        //Debug.Log("Attack cooldown finished");
    }
    #if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
    #endif




}
