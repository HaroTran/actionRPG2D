using UnityEngine;

public class EnemyRangeAttack : EnemyAttack
{
    [SerializeField] protected Transform firePoint;
    //[SerializeField] ProjectileSO projectileSO;

    protected override void ResetAllComponents()
    {
        base.ResetAllComponents();
        firePoint = transform.Find("FirePoint");
    }

    public override void ApplyEnemyAttack()
    {
        Shoot(enemyCtrl.EnemyDectector.CurrentTarget);
    }
    public virtual void Shoot(Transform target)
    {
        if (target == null || firePoint == null) return;

        Vector2 dir = (target.position - firePoint.position).normalized;
        if (dir == Vector2.zero) return;

        ProjectileSpawner spawner = ProjectileSpawner.Instance;
        if (spawner == null) return;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        Transform projectileTransform = spawner.Spawn("Arrow", firePoint.position, rotation);
        if (projectileTransform == null) return;

        ProjectileCtrl projectile = projectileTransform.GetComponent<ProjectileCtrl>();
        if (projectile == null) return;

        if (projectile.Rb != null)
        {
            projectile.Rb.linearVelocity = Vector2.zero;
        }

        projectile.ProjectileFly?.Fly(dir);
    }
}
