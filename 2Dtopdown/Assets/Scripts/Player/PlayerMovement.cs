using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f; 
    private Vector2 movementInput; 
    [SerializeField] private Rigidbody2D rb; 
    [SerializeField] private Animator animator;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        movementInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        animator.SetFloat("horizontal", Mathf.Abs(movementInput.x));
        animator.SetFloat("vertical", Mathf.Abs(movementInput.y));
        if (movementInput.x != 0)
        {
            transform.localScale = new Vector3(Mathf.Sign(movementInput.x), 1, 1);
        }
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = movementInput * moveSpeed;
    }
}
