using System.Collections;
using UnityEngine;

public class EnemyAttack : HaroMonoBehavior
{
    [SerializeField] protected EnemyCtrl enemyCtrl;
    [SerializeField] protected LayerMask targetLayer;
    [SerializeField] protected float attackRange = 1f;

    [SerializeField] protected float attackCooldown = 1f;

    [SerializeField] protected bool isAttacking = false;
    public bool IsAttacking { get { return isAttacking; } }
    [SerializeField] protected bool isCanAttack = true;
    public bool IsCanAttack { get { return isCanAttack; } }
    protected override void ResetAllComponents()
    {
        base.ResetAllComponents();
        if (enemyCtrl == null)
        {
            enemyCtrl = GetComponentInParent<EnemyCtrl>();
        }
        if (enemyCtrl != null && enemyCtrl.EnemyStatsSO != null)
        {
            attackRange = enemyCtrl.EnemyStatsSO.AttackRange;
            attackCooldown = enemyCtrl.EnemyStatsSO.AttackCooldown;
        }

    }


    public void StopAttacking()
    {
        isAttacking = false;
        StartCoroutine(CooldownAttack());
    }

    public virtual void ApplyEnemyAttack()
    {        
        Collider2D hit = Physics2D.OverlapCircle(transform.position, attackRange, targetLayer);
        if(hit == null)
        {
            //Debug.Log("No target in range to attack.");
            return;
        }
        if (hit == null || !hit.CompareTag("Player"))
        {
            return;
        }
        PlayerCtrl player = hit.GetComponent<PlayerCtrl>();
        if (player != null && player.PlayerHealth != null)
        {
            int damage = enemyCtrl.EnemyStatsSO.AttackDamage;
            player.PlayerHealth.TakeDamage(damage);
            //Debug.Log($"Player took {damage} damage from enemy.");
        }
    }
    public void EnemyAttacking()
    {
        if (enemyCtrl.EnemyDectector.CurrentTarget == null)
        {
            isAttacking = false;
        }
        if (isTargetInRange() && isCanAttack)
        {
            //Debug.Log("isTargetInRange");
            isAttacking = true;
        }
        return;
    }
    IEnumerator CooldownAttack()
    {
        //Debug.Log("Start Attack cooldown");
        isCanAttack = false;
        yield return new WaitForSeconds(attackCooldown);
        isCanAttack = true;
        //Debug.Log("Attack cooldown finished");
    }
/*
            IEnumerator Attacking()
            {
                yield return new WaitForSeconds(0.2f);
                enemyCtrl.Animator.SetBool("isAttacking", false);
                yield return new WaitForSeconds(0.5f);
                isAttacking = false;
                Debug.Log("Attack finished");
            }
            */
    public bool isTargetInRange()
    {
        if (enemyCtrl == null || enemyCtrl.EnemyDectector == null || enemyCtrl.EnemyDectector.CurrentTarget == null)
        {
            return false;
        }
        float distance = Vector2.Distance(transform.position, enemyCtrl.EnemyDectector.CurrentTarget.position);
        return distance <= attackRange;
    }

    #if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        var origin = transform.position != null ? transform.position : transform.position;

        Color prevColor = Gizmos.color;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(origin, attackRange);
        Gizmos.color = prevColor;
    }
    #endif
}
