using TMPro;
using Unity.AppUI.UI;
using UnityEngine;
public class SoulUI : HaroMonoBehavior
{
    [SerializeField] protected TextMeshProUGUI soulText;
    public TextMeshProUGUI SoulText { get { return soulText; } }

    protected override void ResetAllComponents()
    {
        base.ResetAllComponents();
        if (soulText == null)
        {
            soulText = GetComponentInChildren<TextMeshProUGUI>();
        }
    }

    private void OnEnable()
    {
        if(SoulManager.Instance!=null)
        {
            SoulManager.Instance.RegisterSoulUI(this);
        }
    }
    public void OnSoulChanged(int current)
    {
        Debug.Log("SoulUI.OnSoulChanged");
        if (soulText != null)
        {
            soulText.text = $"{current}";
        }
    }

}
