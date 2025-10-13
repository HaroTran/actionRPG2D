using TMPro;
using Unity.AppUI.UI;
using UnityEngine;

public class PlayerHealthUI : HaroMonoBehavior
{
    [SerializeField] protected TextMeshProUGUI healthText;
    public TextMeshProUGUI HealthText { get { return healthText; } }
    [SerializeField] protected PlayerHealth playerHealth;
    [SerializeField] protected Animator animator;


    protected override void ResetAllComponents()
    {
        base.ResetAllComponents();
        if (healthText == null)
        {
            healthText = GetComponentInChildren<TextMeshProUGUI>();
        }
        if(animator==null)
        {
            animator = GetComponent<Animator>();
        }

    }
    private void OnEnable()
    {
        if (playerHealth == null) return;
        playerHealth.OnHealthChanged += UpdateHealthText;
    }
    private void OnDisable()
    {
        if (playerHealth == null) return;
        playerHealth.OnHealthChanged -= UpdateHealthText;
    }

    private void UpdateHealthText(int current,int max)
    {
        animator?.SetTrigger("isChangeHealth");
        if (healthText != null)
        {
            healthText.text = $"{current}/{max}";
        }
    }
}
