using UnityEngine;

public class PlayerMoveState : PlayerState
{
    public PlayerMoveState(PlayerCtrl playerCtrl, PlayerStateMachine playerStateMachine) : base(playerCtrl, playerStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        playerCtrl.PlayerAnimator.SetBool("isAttacking", false);
        playerCtrl.PlayerAnimator.SetBool("isMoving", true);
    }

    public override void Exit()
    {
        base.Exit();
        //Debug.Log("Exit Move State");
        playerCtrl.PlayerAnimator.SetBool("isMoving", false);
        
    }

    public override void Tick()
    {
        base.Tick();
        if(playerCtrl.PlayerDash.IsDashing)
        {
            playerStateMachine.ChangeState(playerStateMachine.playerDashState);
            return;
        }
        if (!playerCtrl.PlayerMovement.IsMoving)
        {
            playerStateMachine.ChangeState(playerStateMachine.playerIdleState);
        }
    }
}
