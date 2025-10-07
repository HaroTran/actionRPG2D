using System;
using UnityEngine;

public class PlayerMovement : HaroMonoBehavior
{
    [SerializeField] private PlayerCtrl playerCtrl;
    [SerializeField] private float moveSpeed = 5f; 
    private Vector2 movementInput; 
    [SerializeField] private Rigidbody2D rb; 
    [SerializeField] private Animator animator;

    protected override void Awake()
    {
        base.Awake();
        ResetAllComponents();
    }   
    protected override void ResetAllComponents()
    {
        playerCtrl = GetComponentInParent<PlayerCtrl>();
        if (playerCtrl != null && playerCtrl.PlayerStatsSO != null)
        {
            moveSpeed = playerCtrl.PlayerStatsSO.MoveSpeed;
        }
        base.ResetAllComponents();
        if (rb == null)
        {
            rb = playerCtrl?.PlayerRb;
        }
        if (animator == null)
        {
            animator = playerCtrl?.PlayerAnimator;
        }

    }

    private void Update()
    {
        movementInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        animator.SetFloat("horizontal", Mathf.Abs(movementInput.x));
        animator.SetFloat("vertical", Mathf.Abs(movementInput.y));
        if (movementInput.x != 0)
        {
            playerCtrl.transform.localScale = new Vector3(Mathf.Sign(movementInput.x), 1, 1);
        }
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = movementInput * moveSpeed;
    }
}
