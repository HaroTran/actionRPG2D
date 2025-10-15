/*
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyHealthBarClone : MonoBehaviour
{
    [SerializeField] private int MaxValue=100;
    [SerializeField] private int CurrenValue;

    [SerializeField] private RectTransform topbar;
    [SerializeField] private RectTransform bottombar;


    [SerializeField] private float fullWidth;
    [SerializeField] private float TargetWidth => CurrenValue*fullWidth/MaxValue;
    [SerializeField] float animattionSpeed = 10f;

    private Coroutine adjustBarWithCoroutine;

    [SerializeField] private Transform facingSource;
    private Vector3 initialScale;

    void Start()
    {
        fullWidth = topbar.rect.width;
    }
    void Awake()
    {
        initialScale = transform.localScale;
        if (facingSource == null)
        {
            facingSource = transform.parent;
        }
    }
    private void LateUpdate()
    {
        if (facingSource == null) return;

        float sign = Mathf.Sign(facingSource.lossyScale.x);
        if (Mathf.Approximately(sign, 0f)) sign = 1f;

        Vector3 scale = initialScale;
        scale.x = Mathf.Abs(initialScale.x) * sign;
        transform.localScale = scale;
    }
    public void Change(int amount)
    {
        CurrenValue = Mathf.Clamp(CurrenValue + amount, 0, MaxValue);
        if (adjustBarWithCoroutine != null)
        {
            StopCoroutine(adjustBarWithCoroutine);
        }
        adjustBarWithCoroutine = StartCoroutine(AdjustBarWidth(amount));
    }

    private void Update()
    {
        if (Mouse.current.rightButton.wasPressedThisFrame)
        {
            Change(-20);
        }
    }
    private IEnumerator AdjustBarWidth(int amount)
    {
        var suddenChangeBar = amount >= 0 ? bottombar : topbar;
        var slowChangeBar = amount >= 0 ? topbar : bottombar;
        suddenChangeBar.SetWidth(TargetWidth);
        while (Mathf.Abs(suddenChangeBar.rect.width - slowChangeBar.rect.width) > 1f)
        {
            slowChangeBar.SetWidth(Mathf.Lerp(slowChangeBar.rect.width, TargetWidth, Time.deltaTime * animattionSpeed));
            yield return null;
        }
        slowChangeBar.SetWidth(TargetWidth);
    }


}

    public static class RectTransformExtensions
    {
        public static void SetWidth(this RectTransform rect, float width)
        {
            rect.sizeDelta = new Vector2(width, rect.sizeDelta.y);
        }
    }
    */