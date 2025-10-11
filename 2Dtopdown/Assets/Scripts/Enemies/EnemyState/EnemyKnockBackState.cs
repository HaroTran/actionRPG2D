using UnityEngine;

public class EnemyKnockBackState : EnemyState
{
    public EnemyKnockBackState(EnemyCtrl enemyCtrl, EnemyStateMachine enemyStateMachine) : base(enemyCtrl, enemyStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Enter KnockBack State");
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Tick()
    {
        base.Tick();
    }
}

