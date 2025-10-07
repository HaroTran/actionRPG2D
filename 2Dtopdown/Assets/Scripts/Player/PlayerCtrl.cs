using UnityEngine;

public class PlayerCtrl : ObjectCtrl
{
    [SerializeField] protected DamageReceiver damageReceiver;
    [SerializeField] protected HeathStatsManager heathStatsManager;

    protected new void Reset()
    {
        ResetAllComponents();
    }
    protected override void Awake()
    {
        ResetAllComponents();
    }
    
    protected override void ResetAllComponents()
    {
        damageReceiver = GetComponentInChildren<DamageReceiver>();
        heathStatsManager = GetComponentInChildren<HeathStatsManager>();
    }
}
