using UnityEngine;

public class EnemyMovement : HaroMonoBehavior
{
    [SerializeField] private EnemyCtrl enemyCtrl;
    [SerializeField] private Rigidbody2D rb2d;


    private Vector3 defaultScale;
    private Transform chaseTarget;

    protected override void ResetAllComponents()
    {
        base.ResetAllComponents();
        if (enemyCtrl == null)
        {
            enemyCtrl = GetComponentInParent<EnemyCtrl>();
        }
        if (rb2d == null)
        {
            rb2d = enemyCtrl.GetComponent<Rigidbody2D>();
            if (rb2d != null)
            {
                rb2d.gravityScale = 0;
                rb2d.constraints = RigidbodyConstraints2D.FreezeRotation;
            }
        }
        defaultScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
    }

    private void FixedUpdate()
    {
        UpdateChase();
    }

    private void UpdateChase()
    {

        if (enemyCtrl == null || enemyCtrl.EnemyStatsSO == null || rb2d == null)
        {
            return;
        }

        if (chaseTarget == null)
        {
            StopChasingPlayer();
            return;
        }

        if (enemyCtrl.CurrentState == EnemyState.Move)
        {
            Vector2 direction = (chaseTarget.position - transform.position).normalized;
            rb2d.linearVelocity = direction * enemyCtrl.EnemyStatsSO.MoveSpeed;

            if (!Mathf.Approximately(direction.x, 0f))
            {
                float facingSign = direction.x > 0f ? 1f : -1f;
                Vector3 newScale = defaultScale;
                newScale.x = defaultScale.x * facingSign;
                enemyCtrl.transform.localScale = newScale;
            }
        }
        else
        {
            StopChasingPlayer();
        }
    }

    public void ChasingPlayer(Transform playerTransform)
    {
        enemyCtrl.CurrentState = EnemyState.Move;
        chaseTarget = playerTransform;
        enemyCtrl.Animator.SetBool("isMoving", true);
        UpdateChase();
        Debug.Log("Chasing Player");
    }

    public void StopChasingPlayer()
    {
        //enemyCtrl.CurrentState = EnemyState.Idle;
        chaseTarget = null;
        enemyCtrl.Animator.SetBool("isMoving", false);
        if (rb2d != null)
        {
            rb2d.linearVelocity = Vector2.zero;
        }
    }

}
