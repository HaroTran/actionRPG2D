using System.Collections;
using UnityEngine;

public class PlayerAttack : DamageSender
{
    [SerializeField] private PlayerCtrl playerCtrl;
    [SerializeField] private LayerMask targetLayer;
    [SerializeField] private float attackRange = 1f;

    [SerializeField] private float attackCooldown = 1f;

    [SerializeField] private bool isAttacking = false;
    public bool IsAttacking { get { return isAttacking; } }
    [SerializeField] private bool isCanAttack = true;
    public bool IsCanAttack { get { return isCanAttack; } }
    protected override void ResetAllComponents()
    {
        base.ResetAllComponents();
        if (playerCtrl == null)
        {
            playerCtrl = GetComponentInParent<PlayerCtrl>();
        }
        if (playerCtrl != null && playerCtrl.PlayerStatsSO != null)
        {
            attackRange = playerCtrl.PlayerStatsSO.AttackRange;
            attackCooldown = playerCtrl.PlayerStatsSO.AttackCooldown;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J) && isCanAttack)
        {
            Debug.Log("Player attack");
            isAttacking = true;
            playerCtrl.PlayerStateMachine.ChangeState(new PlayerAttackState(playerCtrl, playerCtrl.PlayerStateMachine));
        }
    }

    public void StopAttacking()
    {
        isAttacking = false;
        StartCoroutine(CooldownAttack());
    }
    IEnumerator CooldownAttack()
    {
        //Debug.Log("Start Attack cooldown");
        isCanAttack = false;
        yield return new WaitForSeconds(attackCooldown);
        isCanAttack = true;
        //Debug.Log("Attack cooldown finished");
    }




}
