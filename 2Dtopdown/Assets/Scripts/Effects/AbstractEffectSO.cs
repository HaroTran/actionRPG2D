using UnityEngine;


public abstract class AbstractEffectSO : ScriptableObject
{
    [SerializeField] protected EffectName effectName;
    public EffectName EffectName => effectName;
    public abstract void ExecuteEffect(ObjectEffect objectEffect, Transform attacker = null);
}

public enum EffectName
{
    None,
    Knockback,
    SpriteFlash
}