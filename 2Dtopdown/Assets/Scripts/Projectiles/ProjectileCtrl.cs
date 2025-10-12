using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ProjectileCtrl : ObjectCtrl
{
    [SerializeField] protected ProjectileStatSO projectileStatSO;
    public ProjectileStatSO ProjectileStatSO { get { return projectileStatSO; } }
    [SerializeField] protected ProjectileFly projectileFly;
    public ProjectileFly ProjectileFly { get { return projectileFly; } }
    [SerializeField] protected ProjectileDamageSender projectileDamageSender;
    public ProjectileDamageSender ProjectileDamageSender { get { return projectileDamageSender; } }
    [SerializeField] protected ProjectileDespawn projectileDespawn;
    public ProjectileDespawn ProjectileDespawn { get { return projectileDespawn; } }
    [SerializeField] protected Rigidbody2D rb;
    public Rigidbody2D Rb { get { return rb; } }
    [SerializeField] protected CapsuleCollider2D capsuleCollider;
    public CapsuleCollider2D CapsuleCollider { get { return capsuleCollider; } }

    protected override void ResetAllComponents()
    {
        base.ResetAllComponents();
        if (rb == null)
        {
            rb = GetComponent<Rigidbody2D>();
            rb.gravityScale = 0;
        }
        if(projectileFly==null)
        {
            projectileFly=GetComponentInChildren<ProjectileFly>();
        }
        if (projectileDamageSender == null)
        {
            projectileDamageSender = GetComponentInChildren<ProjectileDamageSender>();
        }
        if (projectileDamageSender == null)
        {
            projectileDamageSender = GetComponentInChildren<ProjectileDamageSender>();
        }
        if(projectileDespawn==null)
        {
            projectileDespawn=GetComponentInChildren<ProjectileDespawn>();
        }
        if(capsuleCollider==null)
        {
            capsuleCollider=GetComponent<CapsuleCollider2D>();
            //capsuleCollider.isTrigger=true;
        }
        
    }

    protected override void Awake()
    {
        base.Awake();
        ResetAllComponents();
    }
    
    //TODO 
}
