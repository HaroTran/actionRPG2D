using UnityEngine;

public class EnemyRangeMoveState : EnemyMoveState
{
    public EnemyRangeMoveState(EnemyCtrl enemyCtrl, EnemyStateMachine enemyStateMachine) : base(enemyCtrl, enemyStateMachine) { }

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
            enemyCtrl.EnemyMovement.MovingAroundPlayer(target);
            return;
        }
    }
}
