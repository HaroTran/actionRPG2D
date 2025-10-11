using UnityEngine;
using UnityEngine.Playables;

public class PlayerIdleState : PlayerState
{
    public PlayerIdleState(PlayerCtrl playerCtrl, PlayerStateMachine playerStateMachine) : base(playerCtrl, playerStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        playerCtrl.PlayerAnimator.SetBool("isAttacking", false);
        playerCtrl.PlayerAnimator.SetBool("isMoving", false);
        
    }

    public override void Exit()
    {
        base.Exit();
        //Debug.Log("Exit Idle State");
    }

    public override void Tick()
    {
        base.Tick();
        if (playerCtrl.PlayerMovement.IsMoving && !playerCtrl.PlayerAttack.IsAttacking)
        {
            //Debug.Log("Change to Move State");
            playerStateMachine.ChangeState(playerStateMachine.playerMoveState);
        }
    }
}
