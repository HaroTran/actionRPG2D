using UnityEngine;

public class EnemyStateMachine : HaroMonoBehavior
{
    [SerializeField] private EnemyCtrl enemyCtrl;

    [SerializeField] EnemyState _currentState;
    public EnemyState CurrentState { get { return _currentState; } private set { _currentState = value; } }

    [SerializeField] public EnemyIdleState enemyIdleState{get; private set; }
    [SerializeField] public EnemyMoveState enemyMoveState{get; private set; }
    [SerializeField] public EnemyAttackState enemyAttackState{get; private set; }

    protected override void ResetAllComponents()
    {
        base.ResetAllComponents();
        enemyCtrl = GetComponentInParent<EnemyCtrl>();
        enemyIdleState = new EnemyIdleState(enemyCtrl, this);
        enemyMoveState = new EnemyMoveState(enemyCtrl, this);
        enemyAttackState = new EnemyAttackState(enemyCtrl, this);
        ChangeState(enemyIdleState);
    }
    protected override void Awake()
    {
        base.Awake();
        ResetAllComponents();
    }
    public void ChangeState(EnemyState newState)
    {
        if(_currentState?.GetType().Name == newState?.GetType().Name)
        {
            return;
        }
        Debug.Log("From State: " + (_currentState != null ? _currentState.GetType().Name : "null"));
        if (_currentState != null)
        {
            _currentState.Exit();
        }
        _currentState = newState;
        Debug.Log("To State: " + (_currentState != null ? _currentState.GetType().Name : "null"));
        _currentState?.Enter();
    }
    protected void Update() => _currentState?.Tick();
}

public abstract class EnemyState
{
    protected readonly EnemyCtrl enemyCtrl;
    protected readonly EnemyStateMachine enemyStateMachine;

    protected EnemyState(EnemyCtrl enemyCtrl, EnemyStateMachine enemyStateMachine)
    {
        this.enemyCtrl = enemyCtrl;
        this.enemyStateMachine = enemyStateMachine;
    }

    public virtual void Enter() { }
    public virtual void Exit() { }
    public virtual void Tick() { }

}
