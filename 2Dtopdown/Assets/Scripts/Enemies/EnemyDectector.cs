using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class EnemyDectector : HaroMonoBehavior
{
    [SerializeField] private EnemyCtrl enemyCtrl;
    [SerializeField] private CircleCollider2D detectCollider;

    [SerializeField] private Transform currentTarget;
    public Transform CurrentTarget { get { return currentTarget; } }

    protected override void ResetAllComponents()
    {
        base.ResetAllComponents();
        if (enemyCtrl == null)
        {
            enemyCtrl = GetComponentInParent<EnemyCtrl>();
        }
        if (detectCollider == null)
        {
            detectCollider = GetComponent<CircleCollider2D>();
            if (detectCollider != null)
            {
                detectCollider.isTrigger = true;
                if (enemyCtrl != null && enemyCtrl.EnemyStatsSO != null)
                {
                    detectCollider.radius = enemyCtrl.EnemyStatsSO.DetectRadius;
                }
            }
        }
    }

    protected override void Awake()
    {
        base.Awake();
        ResetAllComponents();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //Debug.Log("Player Enter Detect Range");
            currentTarget = collision.transform;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        //Debug.Log("Player Exit Detect Range");
        if (collision.CompareTag("Player"))
        {
            currentTarget = null;
        }
    }

}
