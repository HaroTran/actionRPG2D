using UnityEngine;

public class DamageReceiver : HaroMonoBehavior
{
    [SerializeField] ObjectCtrl objectCtrl;
    protected virtual void LoadObjectCtrl()
    {
        if (objectCtrl == null)
        {
            objectCtrl = GetComponentInParent<ObjectCtrl>();
        }
    }
    protected override void ResetAllComponents()
    {
        base.ResetAllComponents();
        LoadObjectCtrl();
    }
    protected override void Awake()
    {
        base.Awake();
        ResetAllComponents();
    }
}
