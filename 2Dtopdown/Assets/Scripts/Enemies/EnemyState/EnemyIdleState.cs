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
        
        if(enemyCtrl.EnemyAttack.isTargetInRange())
        {
            enemyStateMachine.ChangeState(new EnemyAttackState(enemyCtrl, enemyStateMachine));
            return;
        }
        else
        {
            enemyStateMachine.ChangeState(new EnemyMoveState(enemyCtrl, enemyStateMachine));
            return;
        }
    }
}
