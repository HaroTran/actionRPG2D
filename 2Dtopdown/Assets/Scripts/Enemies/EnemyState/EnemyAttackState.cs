using UnityEngine;

public class EnemyAttackState : EnemyState
{
    public EnemyAttackState(EnemyCtrl enemyCtrl, EnemyStateMachine enemyStateMachine) : base(enemyCtrl, enemyStateMachine) { }

    public override void Enter()
    {
        if(enemyCtrl.EnemyAttack.IsCanAttack == false)
        {
            enemyStateMachine.ChangeState(new EnemyIdleState(enemyCtrl, enemyStateMachine));
            return;
        }
        enemyCtrl.Animator.SetBool("isMoving", false);
    }
    public override void Tick()
    {
        var target = enemyCtrl.EnemyDectector.CurrentTarget;
        if (target == null)
        {
            enemyStateMachine.ChangeState(new EnemyIdleState(enemyCtrl, enemyStateMachine));
            return;
        }

        if (!enemyCtrl.EnemyAttack.isTargetInRange())
        {
            enemyStateMachine.ChangeState(new EnemyMoveState(enemyCtrl, enemyStateMachine));
            return;
        }
        else
        {
            if (enemyCtrl.EnemyAttack.IsCanAttack == false)
            {
                enemyStateMachine.ChangeState(new EnemyIdleState(enemyCtrl, enemyStateMachine));
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
        enemyCtrl.Animator.SetBool("isAttacking", false);
        
    }

}
