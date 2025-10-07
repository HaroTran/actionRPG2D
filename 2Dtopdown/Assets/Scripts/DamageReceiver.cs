using UnityEngine;

public class DamageReceiver : HaroMonoBehavior
{

    protected override void ResetAllComponents()
    {
        base.ResetAllComponents();
    }
    protected override void Awake()
    {
        base.Awake();
        ResetAllComponents();
    }
}
