using System.Collections;
using UnityEngine;

public class ProjectileDespawn : HaroMonoBehavior
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

    protected virtual void OnEnable()
    {
        DespawnbyLifetime();
    }

    public virtual void Despawn()
    {
        ProjectileSpawner.Instance.Despawn(projectileCtrl.transform);
    }

    protected virtual void DespawnbyLifetime()
    {
        if (projectileCtrl == null || projectileCtrl.ProjectileStatSO == null)
        {
            return;
        }
        float lifetime = projectileCtrl.ProjectileStatSO.Lifetime;
        StartCoroutine(StartCoolDownDespawnbyLifetime(lifetime));

    }

    IEnumerator StartCoolDownDespawnbyLifetime(float lifetime)
    {
        yield return new WaitForSeconds(lifetime);
        ProjectileSpawner.Instance.Despawn(projectileCtrl.transform);
    }


}
