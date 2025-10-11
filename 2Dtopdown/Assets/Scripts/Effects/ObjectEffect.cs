using System.Collections.Generic;
using UnityEngine;

public class ObjectEffect : HaroMonoBehavior
{
    [SerializeField] private ObjectCtrl objectCtrl;
    public ObjectCtrl ObjectCtrl => objectCtrl;

    [SerializeField] private List<AbstractEffectSO> effects;

    protected override void ResetAllComponents()
    {
        base.ResetAllComponents();
        if (objectCtrl == null)
        {
            objectCtrl = GetComponentInParent<ObjectCtrl>();
        }
    }

    protected override void Awake()
    {
        base.Awake();
        ResetAllComponents();
    }

    public void PlayEffects(List<EffectName> effectNames, Transform attacker = null)
    {
        if (effectNames == null || effectNames.Count == 0 || effects == null)
        {
            return;
        }

        // Dùng HashSet nếu muốn tránh trùng lặp và tăng tốc tra cứu
        var lookup = new HashSet<EffectName>(effectNames);

        foreach (var effect in effects)
        {
            if (effect != null && lookup.Contains(effect.EffectName))
            {
                effect.ExecuteEffect(this, attacker);
            }
        }
    }
    
    public void PlayEffects(EffectName effectName, Transform attacker = null)
    {
        PlayEffects(new List<EffectName>() { effectName }, attacker);
    }
}
    


