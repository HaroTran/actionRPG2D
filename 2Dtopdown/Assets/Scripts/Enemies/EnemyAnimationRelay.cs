using UnityEngine;


//dùng tạm để điều khiển animation event
public class EnemyAnimationRelay : HaroMonoBehavior
{
    [SerializeField] private EnemyCtrl enemyCtrl;

    protected override void ResetAllComponents()
    {
        base.ResetAllComponents();
        if (enemyCtrl == null)
        {
            enemyCtrl = GetComponent<EnemyCtrl>();
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
        if (enemyCtrl != null && enemyCtrl.EnemyAttack != null)
        {
            enemyCtrl.EnemyAttack.ApplyEnemyAttack();
        }
    }
}
