using UnityEngine;

public class EnemyStateMachine : HaroMonoBehavior
{
    [SerializeField] private EnemyCtrl enemyCtrl;

    [SerializeField] EnemyState _currentState;
    public EnemyState CurrentState { get { return _currentState; } private set { _currentState = value; } }

    [SerializeField] protected EnemyIdleState enemyIdleState;
    [SerializeField] protected EnemyMoveState enemyMoveState;
    [SerializeField] protected EnemyAttackState enemyAttackState;

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
        if (_currentState != null)
        {
            _currentState.Exit();
        }
        _currentState = newState;
        _currentState?.Enter();
    }
    protected void Update() => _currentState?.Tick();
}

public abstract class EnemyState : MonoBehaviour
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
