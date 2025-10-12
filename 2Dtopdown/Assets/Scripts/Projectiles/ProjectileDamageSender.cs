using UnityEngine;

public class ProjectileDamageSender : DamageSender
{
    [SerializeField] protected ProjectileCtrl projectileCtrl;

    [SerializeField] protected CapsuleCollider2D projectileCollider;
    protected override void ResetAllComponents()
    {
        base.ResetAllComponents();
        if (projectileCtrl == null)
        {
            projectileCtrl = GetComponentInParent<ProjectileCtrl>();
        }
        if (projectileCollider == null)
        {
            projectileCollider = GetComponent<CapsuleCollider2D>();
            projectileCollider.isTrigger = true;
        }

    }
    protected override void Awake()
    {
        base.Awake();
        ResetAllComponents();
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if(collision!=null)
            {
                collision.GetComponent<PlayerCtrl>().PlayerHealth.TakeDamage(projectileCtrl.ProjectileStatSO.Damage);
                projectileCtrl.ProjectileDespawn.Despawn();
            }
        }
    }

}
