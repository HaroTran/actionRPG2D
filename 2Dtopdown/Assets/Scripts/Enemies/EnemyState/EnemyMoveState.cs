using UnityEngine;

public class EnemyMoveState : EnemyState
{
    public EnemyMoveState(EnemyCtrl enemyCtrl, EnemyStateMachine enemyStateMachine) : base(enemyCtrl, enemyStateMachine) { }

    public override void Enter()
    {
        enemyCtrl.Animator.SetBool("isMoving", true);
        enemyCtrl.Animator.SetBool("isAttacking", false);
    }

    public override void Tick()
    {
        var target = enemyCtrl.EnemyDectector.CurrentTarget;
        if (target == null)
        {
            enemyStateMachine.ChangeState(new EnemyIdleState(enemyCtrl, enemyStateMachine));
            enemyCtrl.EnemyMovement.StopChasingPlayer();
            return;
        }

        if (enemyCtrl.EnemyAttack.isTargetInRange())
        {
            enemyCtrl.EnemyMovement.StopChasingPlayer();
            enemyStateMachine.ChangeState(new EnemyAttackState(enemyCtrl, enemyStateMachine));
            return;
        }
        else
        {
            enemyCtrl.EnemyMovement.ChasingPlayer(target);
            return;
        }
    }
    public override void Exit()
    {
        enemyCtrl.EnemyMovement.StopChasingPlayer();
        enemyCtrl.Animator.SetBool("isMoving", false);
    }

}
