using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "NewKnockbackEffect", menuName = "EffectsSO/Data/Knockback Effect")]
public class KnockbackEffectSO : AbstractEffectSO
{
    private void OnEnable()
    {
        effectName = EffectName.Knockback;
    }
    #if UNITY_EDITOR
    private void OnValidate()
    {
        effectName = EffectName.Knockback;
    }
#endif
    [SerializeField] private float knockbackForce = 5f;
    [SerializeField] private float knockbackDuration = 0.15f;
    public override void ExecuteEffect(ObjectEffect objectEffect, Transform attacker = null)
    {
        Rigidbody2D rb2d = objectEffect.ObjectCtrl.GetComponent<Rigidbody2D>();
        ObjectStateMachine stateMachine=objectEffect.ObjectCtrl.GetComponentInChildren<ObjectStateMachine>();
        if (rb2d != null && attacker != null && stateMachine != null)
        {
            stateMachine.EnterKnockbackState();
            objectEffect.StartCoroutine(KnockbackCoroutine(rb2d,stateMachine, attacker));
        }

    }
    
    IEnumerator KnockbackCoroutine(Rigidbody2D rb2d ,ObjectStateMachine objectStateMachine,Transform attacker)
    {
        
        Debug.Log(rb2d.transform.position+" knocked back by "+attacker.position);
        Vector2 direction = (rb2d.transform.position - attacker.position).normalized;
        rb2d.linearVelocity = direction * knockbackForce;
        yield return new WaitForSeconds(knockbackDuration);
        rb2d.linearVelocity = Vector2.zero;
        objectStateMachine.ExitKnockbackState();
    }
}
