using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "NewSpriteFlashEffect", menuName = "EffectsSO/Data/Sprite Flash Effect")]
public class SpriteFlashEffectSO : AbstractEffectSO
{
    private void OnEnable()
    {
        effectName = EffectName.SpriteFlash;
    }
#if UNITY_EDITOR
    private void OnValidate()
    {
        effectName = EffectName.SpriteFlash;
    }
#endif

    [SerializeField] private float flashDuration = 0.1f;
    [SerializeField] private Material flashMaterial;

    public override void ExecuteEffect(ObjectEffect objectEffect, Transform attacker = null)
    {
        SpriteRenderer spriteRenderer = objectEffect.ObjectCtrl.GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            Material originalMaterial = spriteRenderer.material;
            objectEffect.StartCoroutine(FlashCoroutine(spriteRenderer, originalMaterial));
        }
    }
    IEnumerator FlashCoroutine(SpriteRenderer spriteRenderer, Material originalMaterial)
    {
        spriteRenderer.material = flashMaterial;
        yield return new WaitForSeconds(flashDuration);
        spriteRenderer.material = originalMaterial;
    }
}
