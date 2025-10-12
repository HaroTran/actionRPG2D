using UnityEngine;

public class EnemyStateMachine : ObjectStateMachine
{
    [SerializeField] private EnemyCtrl enemyCtrl;

    [SerializeField] EnemyState _currentState;
    public EnemyState CurrentState { get { return _currentState; } private set { _currentState = value; } }

    [SerializeField] public EnemyIdleState enemyIdleState{get; private set; }
    [SerializeField] public EnemyMoveState enemyMoveState{get; private set; }
    [SerializeField] public EnemyAttackState enemyAttackState { get; private set; }
    [SerializeField] public EnemyKnockBackState enemyKnockBackState { get; private set; }
    [SerializeField] public EnemyRangeMoveState enemyRangeMoveState { get; private set; }

    protected override void ResetAllComponents()
    {
        base.ResetAllComponents();
        enemyCtrl = GetComponentInParent<EnemyCtrl>();
        enemyIdleState = new EnemyIdleState(enemyCtrl, this);
        enemyMoveState = new EnemyMoveState(enemyCtrl, this);
        enemyAttackState = new EnemyAttackState(enemyCtrl, this);
        enemyKnockBackState = new EnemyKnockBackState(enemyCtrl, this);
        enemyRangeMoveState = new EnemyRangeMoveState(enemyCtrl, this);
        ChangeState(enemyIdleState);
    }
    protected override void Awake()
    {
        base.Awake();
        ResetAllComponents();
    }
    public void ChangeState(EnemyState newState)
    {

        //Debug.Log("From State: " + (_currentState != null ? _currentState.GetType().Name : "null"));
        if (_currentState != null)
        {
            _currentState.Exit();
        }
        _currentState = newState;
        //Debug.Log("To State: " + (_currentState != null ? _currentState.GetType().Name : "null"));
        _currentState?.Enter();
    }
    protected void Update() => _currentState?.Tick();

    // Implement abstract methods from ObjectStateMachine
    public override void EnterKnockbackState()
    {
        ChangeState(enemyKnockBackState);
    }

    public override void ExitKnockbackState()
    {
        // You can choose which state to transition to after knockback, here we use Idle as an example
        ChangeState(enemyIdleState);
    }
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
