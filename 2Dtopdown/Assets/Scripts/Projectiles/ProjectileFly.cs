using Unity.VisualScripting;
using UnityEngine;

public class ProjectileFly : HaroMonoBehavior
{
    [SerializeField] protected ProjectileCtrl projectileCtrl;

    protected override void ResetAllComponents()
    {
        base.ResetAllComponents();
        if (projectileCtrl == null)
        {
            projectileCtrl = GetComponentInParent<ProjectileCtrl>();
        }
    }
    
    public void Fly(Vector2 dir)
    {
        Rigidbody2D rb = projectileCtrl.Rb;
        if (rb != null)
        {
            rb.linearVelocity = dir * projectileCtrl.ProjectileStatSO.Speed;
        }
    }

}
    
