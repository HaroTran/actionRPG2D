using UnityEngine;

public class DamageSender : HaroMonoBehavior
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
