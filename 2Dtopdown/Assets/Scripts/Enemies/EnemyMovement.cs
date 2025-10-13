using UnityEngine;

public class EnemyMovement : HaroMonoBehavior
{
    [SerializeField] private EnemyCtrl enemyCtrl;
    [SerializeField] private Rigidbody2D rb2d;


    private Vector3 defaultScale;
    private Transform chaseTarget;
    protected Vector2 moveDirection;

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


    public void ChasingPlayer(Transform chaseTarget)
    {
        this.chaseTarget = chaseTarget;

        if (enemyCtrl == null || enemyCtrl.EnemyStatsSO == null || rb2d == null)
        {
            return;
        }
        moveDirection = (chaseTarget.position - transform.position).normalized;
        rb2d.linearVelocity = moveDirection * enemyCtrl.EnemyStatsSO.MoveSpeed;
        SwapFaceDirection(moveDirection);

    }
    /*
    public void MovingAroundPlayer(Transform chaseTarget)
    {
        this.chaseTarget = chaseTarget;
        if (enemyCtrl == null || enemyCtrl.EnemyStatsSO == null || rb2d == null || chaseTarget == null)
        {
            return;
        }
        float desiredDistance = Mathf.Max(0.1f, enemyCtrl.EnemyStatsSO.AttackRange);
        Vector2 toTarget = (Vector2)(chaseTarget.position - transform.position);
        float currentDistance = toTarget.magnitude;
        Vector2 moveDirection;
        if (currentDistance < desiredDistance * 0.65f)
        {
            moveDirection = -toTarget.normalized;
        }
        else if (currentDistance > desiredDistance * 1.1f)
        {
            moveDirection = toTarget.normalized;
        }
        else
        {
            Vector2 tangent = new Vector2(-toTarget.y, toTarget.x);
            if (tangent.sqrMagnitude < 0.0001f)
            {
                tangent = Random.insideUnitCircle;
            }
            tangent = tangent.normalized;
            float directionSign = Random.value < 0.5f ? -1f : 1f;
            moveDirection = tangent * directionSign;
        }
        rb2d.linearVelocity = moveDirection * enemyCtrl.EnemyStatsSO.MoveSpeed;
        SwapFaceDirection(moveDirection);
    }*/

    public void SwapFaceDirection(Vector2 direction)
    {
        moveDirection = direction;
        if (!Mathf.Approximately(moveDirection.x, 0f))
        {
            float facingSign = moveDirection.x > 0f ? 1f : -1f;
            Vector3 newScale = defaultScale;
            newScale.x = defaultScale.x * facingSign;
            enemyCtrl.transform.localScale = newScale;
        }
    }
    public void SwapFaceDirection()
    {
        if (enemyCtrl.EnemyDectector.CurrentTarget != null)
        {
            //Debug.Log("SwapFaceDirection TO player");
            moveDirection = (enemyCtrl.EnemyDectector.CurrentTarget.position - transform.position).normalized;
            SwapFaceDirection(moveDirection);
        }

    }

    public void StopChasingPlayer()
    {
        if (rb2d != null)
        {
            rb2d.linearVelocity = Vector2.zero;
        }
    }

}
