using UnityEngine;

public class EnemyIdleState : EnemyState
{
    public EnemyIdleState(EnemyCtrl enemyCtrl, EnemyStateMachine enemyStateMachine) : base(enemyCtrl, enemyStateMachine) { }

    public override void Enter()
    {
        enemyCtrl.Animator.SetBool("isMoving", false);
        enemyCtrl.Animator.SetBool("isAttacking", false);
    }

    public override void Tick()
    {
        var target = enemyCtrl.EnemyDectector.CurrentTarget;
        if (target == null)
        {
            return;
        }

        if (enemyCtrl.EnemyAttack.isTargetInRange())
        {
            if(enemyCtrl.EnemyAttack.IsCanAttack == false)
            {
                return;
            }
            enemyStateMachine.ChangeState(enemyStateMachine.enemyAttackState);
            return;
        }
        else
        {
            enemyStateMachine.ChangeState(enemyStateMachine.enemyMoveState);
            return;
        }
    }
    public override void Exit()
    {
        Debug.Log("Exit Enemy Idle State");
    }
}
