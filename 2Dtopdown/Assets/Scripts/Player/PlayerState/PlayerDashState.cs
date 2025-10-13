using UnityEngine;

public class PlayerDashState : PlayerState
{
    public PlayerDashState(PlayerCtrl playerCtrl, PlayerStateMachine playerStateMachine) : base(playerCtrl, playerStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        playerCtrl.PlayerMovement.DisableMovement(false);
        playerCtrl.PlayerAnimator.SetBool("isMoving", false);
    }
    public override void Tick()
    {
        base.Tick();
        if(playerCtrl.PlayerDash.IsDashing == false)
        {
            playerStateMachine.ChangeState(playerStateMachine.playerIdleState);
        }
    }
    public override void Exit()
    {
        base.Exit();
        playerCtrl.PlayerMovement.EnableMovement();
    }
}
