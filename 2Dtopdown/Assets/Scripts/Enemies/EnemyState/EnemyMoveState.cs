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
            enemyStateMachine.ChangeState(enemyStateMachine.enemyIdleState);
            enemyCtrl.EnemyMovement.StopChasingPlayer();
            return;
        }

        if (enemyCtrl.EnemyAttack.isTargetInRange())
        {
            enemyCtrl.EnemyMovement.StopChasingPlayer();
            enemyStateMachine.ChangeState(enemyStateMachine.enemyAttackState);
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
        //Debug.Log("Exit Enemy Move State");
        enemyCtrl.EnemyMovement.StopChasingPlayer();
        enemyCtrl.Animator.SetBool("isMoving", false);
    }

}
