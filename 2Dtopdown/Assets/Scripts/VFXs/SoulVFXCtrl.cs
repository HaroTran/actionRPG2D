using System.Collections;
using UnityEngine;

public class SoulVFXCtrl : VFXCtrl
{
    [SerializeField] private float moveSpeed = 8f;
    [SerializeField] private float moveDelay = 1f;
    [SerializeField] private Animator animator;
    private Coroutine moveRoutine;
    private static Transform playerTransformCache;

    [SerializeField] private int SoulValue = 0;
    protected override void ResetAllComponents()
    {
        base.ResetAllComponents();
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
    }
    public void SetSoulValue(int amount)
    {
        SoulValue = amount;
    }
    protected virtual void OnEnable()
    {
        if (moveRoutine != null)
        {
            StopCoroutine(moveRoutine);
        }
        moveRoutine = StartCoroutine(MoveTowardsPlayer());
    }
    protected virtual void OnDisable()
    {
        if (moveRoutine != null)
        {
            StopCoroutine(moveRoutine);
            moveRoutine = null;
        }
    }
    private IEnumerator MoveTowardsPlayer()
    {
        animator?.SetBool("isMoving", true);
        yield return new WaitForSeconds(moveDelay);

        var playerTransform = GetPlayerTransform();
        var toPlayer = playerTransform.position - transform.position;

        if (toPlayer.sqrMagnitude > Mathf.Epsilon)
        {
            float angle = Mathf.Atan2(toPlayer.y, toPlayer.x) * Mathf.Rad2Deg;
            // điều chỉnh offset nếu prefab của bạn mặc định hướng lên/xuống
            transform.rotation = Quaternion.Euler(0f, 0f, angle-200f);
        }

        if (playerTransform == null)
        {
            moveRoutine = null;
            yield break;
        }
        while (isActiveAndEnabled && playerTransform != null)
        {
            //Vector3 direction = (playerTransform.position - transform.position).normalized;
            transform.position = Vector3.MoveTowards(
                transform.position,
                playerTransform.position,
                moveSpeed * Time.deltaTime);
            if ((transform.position - playerTransform.position).sqrMagnitude <= 0.01f)
            {
                animator?.SetBool("isMoving", false);
                yield return new WaitForSeconds(0.1f);
                moveRoutine = null;
                SoulManager.Instance.AddSoul(SoulValue);
                gameObject.SetActive(false);
                yield break;
            }
            yield return null;
        }
        moveRoutine = null;
    }
    private static Transform GetPlayerTransform()
    {
        if (playerTransformCache != null && playerTransformCache.gameObject.activeInHierarchy)
        {
            return playerTransformCache;
        }
        GameObject playerObj = GameObject.FindWithTag("Player");
        if (playerObj == null)
        {
            return null;
        }
        playerTransformCache = playerObj.transform;
        return playerTransformCache;
    }
}
