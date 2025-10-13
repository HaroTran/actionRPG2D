using System.Collections;
using UnityEngine;

public class PlayerDash : HaroMonoBehavior
{
    [SerializeField] protected PlayerCtrl playerCtrl;

    [SerializeField] protected Rigidbody2D rb;
    [SerializeField] protected bool isCanDash = true;
    [SerializeField] protected bool isDashing = false;
    public bool IsCanDash { get { return isCanDash; } }
    public bool IsDashing { get { return isDashing; } }
    [SerializeField] protected float dashCoolDown = 1.5f;
    [SerializeField] protected float dashSpeed = 15f;
    [SerializeField] protected float dashTime = 1f;

    private Coroutine dashRoutine;
    private Coroutine cooldownRoutine;
    private Vector2 cachedDirection;

    [Header("Ghost Effect")]
    [SerializeField] protected GameObject ghostEffect;
    [SerializeField] protected float ghostDelaytime = 0.1f;
    private Coroutine ghostEffectCoroutine;



    protected override void ResetAllComponents()
    {
        base.ResetAllComponents();
        if (playerCtrl == null)
        {
            playerCtrl = GetComponentInParent<PlayerCtrl>();
        }
        if (rb == null)
        {
            rb = playerCtrl?.PlayerRb;
        }

    }
    private void Update()
    {
        if (!isCanDash || isDashing)
        {
            return;
        }

        var input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        if (input != Vector2.zero && Input.GetKeyDown(KeyCode.L))
        {
            StartDash(input);
        }
    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            Debug.Log("Dashing");
            rb.linearVelocity = cachedDirection * dashSpeed;
        }
    }

    private void StartDash(Vector2 direction)
    {
        Debug.Log("Start Dash");
        if (dashRoutine != null)
        {
            StopCoroutine(dashRoutine);
        }

        cachedDirection = direction;
        dashRoutine = StartCoroutine(DashRoutine());
        StartGhostEffect();


    }
    private void StartGhostEffect()
    {
        if (ghostEffectCoroutine != null)
        {
            StopCoroutine(ghostEffectCoroutine);
        }
        ghostEffectCoroutine = StartCoroutine(GhostEffectCoroutine());
    }
    private void StopGhostEffect()
    {
        if (ghostEffectCoroutine != null)
        {
            StopCoroutine(ghostEffectCoroutine);
        }
    }



    private IEnumerator DashRoutine()
    {
        isCanDash = false;
        isDashing = true;

        var movement = playerCtrl.PlayerMovement;
        movement?.DisableMovement(false);   // không stop velocity ngay

        yield return new WaitForSeconds(dashTime);

        isDashing = false;
        rb.linearVelocity = Vector2.zero;
        movement?.EnableMovement();

        if (cooldownRoutine != null)
        {
            StopCoroutine(cooldownRoutine);
        }
        StopGhostEffect();
        cooldownRoutine = StartCoroutine(DashCooldown());
    }

    private IEnumerator DashCooldown()
    {
        yield return new WaitForSeconds(dashCoolDown);
        isCanDash = true;
    }

    private IEnumerator GhostEffectCoroutine()
    {
        while(true)
        {
            GameObject ghost = Instantiate(ghostEffect, playerCtrl.transform.position, Quaternion.identity);
            Destroy(ghost, dashTime-ghostDelaytime);
            yield return new WaitForSeconds(ghostDelaytime);
        }
    }


}
