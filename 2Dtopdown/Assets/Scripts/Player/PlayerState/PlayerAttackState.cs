using UnityEngine;
using UnityEngine.Playables;

public class PlayerAttackState : PlayerState
{
    public PlayerAttackState(PlayerCtrl playerCtrl, PlayerStateMachine playerStateMachine) : base(playerCtrl, playerStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        playerCtrl.PlayerMovement.DisableMovement();
        playerCtrl.PlayerAnimator.SetBool("isAttacking", true);
        playerCtrl.PlayerAnimator.SetBool("isMoving", false);
    }

    public override void Tick()
    {
        base.Tick();
        if(playerCtrl.PlayerAttack.IsAttacking == true)
        {
            return;
        }
        if (playerCtrl.PlayerMovement.IsMoving)
        {
            //Debug.Log("Change to Move State");
            playerStateMachine.ChangeState(playerStateMachine.playerMoveState);
            return;
        }
        else
        {
            //Debug.Log("Change to Idle State");
            playerStateMachine.ChangeState(playerStateMachine.playerIdleState);
        }
    }
    public override void Exit()
    {
        base.Exit();
        //Debug.Log("Exit Attack State");
        playerCtrl.PlayerMovement.EnableMovement();
        playerCtrl.PlayerAnimator.SetBool("isAttacking", false);
    }
    
}
