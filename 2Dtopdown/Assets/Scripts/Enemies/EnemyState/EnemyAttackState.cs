using UnityEngine;

public class EnemyAttackState : EnemyState
{
    public EnemyAttackState(EnemyCtrl enemyCtrl, EnemyStateMachine enemyStateMachine) : base(enemyCtrl, enemyStateMachine) { }

    public override void Enter()
    {
        if (enemyCtrl.EnemyAttack.IsCanAttack == false)
        {
            enemyStateMachine.ChangeState(enemyStateMachine.enemyIdleState);
            return;
        }
        enemyCtrl.Animator.SetBool("isMoving", false);
    }
    public override void Tick()
    {
        if (enemyCtrl.EnemyAttack.IsAttacking)
        {
            return;
        }
        var target = enemyCtrl.EnemyDectector.CurrentTarget;
        if (target == null)
        {
            enemyStateMachine.ChangeState(enemyStateMachine.enemyIdleState);
            return;
        }

        if (!enemyCtrl.EnemyAttack.isTargetInRange())
        {
            enemyStateMachine.ChangeState(enemyStateMachine.enemyMoveState);
            return;
        }
        else
        {
            if (enemyCtrl.EnemyAttack.IsCanAttack == false )
            {
                enemyStateMachine.ChangeState(enemyStateMachine.enemyIdleState);
                return;
            }
            else
            {
                enemyCtrl.EnemyAttack.EnemyAttacking();
                enemyCtrl.Animator.SetBool("isAttacking", true);
                return;
            }
        }
    }
    public override void Exit()
    {
        Debug.Log("Exit Enemy Attack State");
        enemyCtrl.Animator.SetBool("isAttacking", false);
    }

}
