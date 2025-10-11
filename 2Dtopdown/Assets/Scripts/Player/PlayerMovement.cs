using System;
using UnityEngine;

public class PlayerMovement : HaroMonoBehavior
{
    [SerializeField] private PlayerCtrl playerCtrl;
    [SerializeField] private float moveSpeed = 5f;
    private Vector2 movementInput;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator animator;

    [SerializeField] private bool isMoving = true;
    [SerializeField] private bool isCanMove = true;
    public bool IsCanMove { get { return isCanMove; } }
    public bool IsMoving { get { return isMoving; } }
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
        if (movementInput == Vector2.zero)
        {
            isMoving = false;
        }
        else
        {
            isMoving = true;
        }
        if (movementInput.x != 0 && isCanMove)
        {
            playerCtrl.transform.localScale = new Vector3(Mathf.Sign(movementInput.x), 1, 1);
        }
    }

    private void FixedUpdate()
    {
        if (isCanMove && isMoving)
        {
            PlayerMovementStart();
        }
        else
        {
            PlayerMovementStop();
        }
    }
    protected void PlayerMovementStart()
    {
        rb.linearVelocity = movementInput * moveSpeed;
    }
    public void PlayerMovementStop()
    {
        rb.linearVelocity = Vector2.zero;
        isMoving = false;
    }

    public void DisableMovement()
    {
        isCanMove = false;
        PlayerMovementStop();
    }
    public void EnableMovement()
    {
        isCanMove = true;
    }
}
