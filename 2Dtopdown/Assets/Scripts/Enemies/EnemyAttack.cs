using System.Collections;
using UnityEngine;

public class EnemyAttack : HaroMonoBehavior
{
    [SerializeField] private EnemyCtrl enemyCtrl;
    [SerializeField] private LayerMask targetLayer;
    [SerializeField] private float attackRange = 1f;

    [SerializeField] private float attackCooldown = 1f;


    [SerializeField] private bool isAttacking = false;
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
        }
    }

    private void Update()
    {
         UpdateAttack();
    }

    private void UpdateAttack()
    {
        if (isAttacking == true)
        {
            return;
        }
        if (enemyCtrl.EnemyDectector.CurrentTarget == null)
        {
            isAttacking = false;
            enemyCtrl.CurrentState = EnemyState.Idle;
            enemyCtrl.Animator.SetBool("isAttacking", false);
            return ;
        }
        if (isTargetInRange())
        {
            Debug.Log("isTargetInRange");
            isAttacking = true;
            enemyCtrl.CurrentState = EnemyState.Attack;
            enemyCtrl.Animator.SetBool("isAttacking", true);
            StartCoroutine(Attacking());
        }
        return ;
    }

    IEnumerator Attacking()
    {
        yield return new WaitForSeconds(0.2f);
        enemyCtrl.Animator.SetBool("isAttacking", false);
        yield return new WaitForSeconds(0.5f);
        isAttacking = false;
        Debug.Log("Attack finished");
    }
    protected bool isTargetInRange()
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
