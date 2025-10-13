using System.Collections;
using UnityEngine;

public class EnemyBoomExplodeAttack : EnemyAttack
{
    [SerializeField] protected Transform explosion;
    [SerializeField] protected float timetoExplode = 1.5f;



    public override void EnemyAttacking()
    {
        base.EnemyAttacking();
        ExplodeAttack();
    }

    public virtual void ExplodeAttack()
    {
        StartCoroutine(CountDowntoExplode());
    }
    IEnumerator CountDowntoExplode()
    {
        yield return new WaitForSeconds(timetoExplode);
        Transform _explosion = Instantiate(explosion, enemyCtrl.transform.position, Quaternion.identity);
        if (_explosion != null)
        {
            _explosion.gameObject.SetActive(true);
        }
        StopAttacking();
        StopCoroutine(CountDowntoExplode());
    }
}
