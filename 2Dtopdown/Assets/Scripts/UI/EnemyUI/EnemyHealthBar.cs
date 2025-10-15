using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyHealthBar : HaroMonoBehavior
{
    [Header("Health Bar configure")]
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
    [Header("Health Bar function")]
    [SerializeField] protected EnemyCtrl enemyCtrl;


    void Start()
    {
        fullWidth = topbar.rect.width;
        if (enemyCtrl.EnemyHealth != null)
        {
            MaxValue = enemyCtrl.EnemyHealth.MaxHealth;
            CurrenValue = MaxValue;
            enemyCtrl.EnemyHealth.OnHealthChanged += OnHealthChange;
        }
        gameObject.SetActive(false);
        
    }

    private void OnEnable()
    {
        if (enemyCtrl.EnemyHealth != null)
        {
            MaxValue = enemyCtrl.EnemyHealth.MaxHealth;
            CurrenValue = enemyCtrl.EnemyHealth.MaxHealth;
        }
    }

    // tạo sẳn Destroy để dùng sau
    private void OnDestroy()
    {
        if(enemyCtrl.EnemyHealth!=null)
        {
            enemyCtrl.EnemyHealth.OnHealthChanged -= OnHealthChange;
        }
    }
    protected override void ResetAllComponents()
    {
        base.ResetAllComponents();
        if(enemyCtrl==null)
        {
            enemyCtrl = GetComponentInParent<EnemyCtrl>();
        }
    }
    protected override void Awake()
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

    private Coroutine hideHealthBarCoutine;
    private void OnHealthChange(int current, int max)
    {
        this.gameObject.SetActive(true);
        Change(-(CurrenValue - current));
        if (hideHealthBarCoutine != null)
        {
            StopCoroutine(hideHealthBarCoutine);
        }
        hideHealthBarCoutine = StartCoroutine(HideHealthBar());

    }
    private IEnumerator HideHealthBar()
    {
        yield return new WaitForSeconds(0.75f);
        this.gameObject.SetActive(false);
    }

}

    public static class RectTransformExtensions
    {
        public static void SetWidth(this RectTransform rect, float width)
        {
            rect.sizeDelta = new Vector2(width, rect.sizeDelta.y);
        }
    }