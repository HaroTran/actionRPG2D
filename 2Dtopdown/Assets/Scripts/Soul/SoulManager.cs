using System;
using UnityEngine;

public class SoulManager : HaroMonoBehavior
{
    public static SoulManager Instance { get; private set; }
    [SerializeField] protected int soulAmount;
    public int SoulAmount { get { return soulAmount; } }

    public event Action<int> OnSoulChanged;
    protected override void Awake()
    {
        base.Awake();
        if (Instance != null)
        {
            Debug.Log("only one instance of SoulControler allowed");
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    public void AddSoul(int amount)
    {
        soulAmount += amount;
        OnSoulChanged?.Invoke(soulAmount);
    }
    public void UseSoul(int amount)
    {
        if(soulAmount-amount<0)
        {
            Debug.Log("Not enough soul");
            return;
        }
        soulAmount -= amount;
        OnSoulChanged?.Invoke(soulAmount);
    }
    public void RegisterSoulUI(SoulUI soulUI)
    {
        OnSoulChanged += soulUI.OnSoulChanged;
        soulUI.OnSoulChanged(soulAmount);
    }
}
