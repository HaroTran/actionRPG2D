using UnityEngine;

public class PlayerCtrl : ObjectCtrl
{
    [SerializeField] private PlayerStatsSO playerStatsSO;
    public PlayerStatsSO PlayerStatsSO { get { return playerStatsSO; } }
    [SerializeField] private PlayerHealth playerHealth;
    public PlayerHealth PlayerHealth { get { return playerHealth; } }
    [SerializeField] private PlayerMovement playerMovement;
    public PlayerMovement PlayerMovement { get { return playerMovement; } }
    [SerializeField] private PlayerAttack playerAttack;
    public PlayerAttack PlayerAttack { get { return playerAttack; } }
    [SerializeField] private PlayerStateMachine playerStateMachine;
    public PlayerStateMachine PlayerStateMachine { get { return playerStateMachine; } }
    [SerializeField] private Rigidbody2D rb; 
    public Rigidbody2D PlayerRb { get { return rb; } }
    [SerializeField] private Animator animator;
    public Animator PlayerAnimator { get { return animator; } }
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
        base.ResetAllComponents();
        if (playerHealth == null)
        {
            playerHealth = GetComponentInChildren<PlayerHealth>();
        }
        if (playerMovement == null)
        {
            playerMovement = GetComponentInChildren<PlayerMovement>();
        }
        if (playerAttack == null)
        {
            playerAttack = GetComponentInChildren<PlayerAttack>();
        }
        if (playerStateMachine == null)
        {
            playerStateMachine = GetComponentInChildren<PlayerStateMachine>();
        }
        if (rb == null)
        {
            rb = GetComponent<Rigidbody2D>();
        }
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
    }
}
