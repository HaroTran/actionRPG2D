using UnityEngine;

public class PlayerAnimationRelay : HaroMonoBehavior
{
    [SerializeField] private PlayerCtrl playerCtrl;

    protected override void ResetAllComponents()
    {
        base.ResetAllComponents();
        if (playerCtrl == null)
        {
            playerCtrl = GetComponent<PlayerCtrl>();
        }
    }

    protected override void Awake()
    {
        base.Awake();
        ResetAllComponents();
    }

    // Animation Event
    public void OnAttackAnimationEnd()
    {
        if (playerCtrl != null && playerCtrl.PlayerAttack != null)
        {
            playerCtrl.PlayerAttack.StopAttacking();
        }
    }
}
