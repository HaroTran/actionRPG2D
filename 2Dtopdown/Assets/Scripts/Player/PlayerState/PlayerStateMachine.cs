using UnityEngine;

public class PlayerStateMachine : ObjectStateMachine
{
    [SerializeField] private PlayerCtrl playerCtrl;

    public override void EnterKnockbackState()
    {
        // Implement logic for entering knockback state here
    }

    public override void ExitKnockbackState()
    {
        // Implement logic for exiting knockback state here
    }

    [SerializeField] PlayerState _currentState;
    public PlayerState CurrentState { get { return _currentState; } private set { _currentState = value; } }

    [SerializeField] public PlayerIdleState playerIdleState{get; private set; }
    [SerializeField] public PlayerMoveState playerMoveState{get; private set; }
    [SerializeField] public PlayerAttackState playerAttackState{get; private set; }

    protected override void ResetAllComponents()
    {
        base.ResetAllComponents();
        playerCtrl = GetComponentInParent<PlayerCtrl>();
        playerIdleState = new PlayerIdleState(playerCtrl, this);
        playerMoveState = new PlayerMoveState(playerCtrl, this);
        playerAttackState = new PlayerAttackState(playerCtrl, this);
        ChangeState(playerIdleState);
    }
    protected override void Awake()
    {
        base.Awake();
        ResetAllComponents();
    }
    public void ChangeState(PlayerState newState)
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
public abstract class PlayerState
{
    protected readonly PlayerCtrl playerCtrl;
    protected readonly PlayerStateMachine playerStateMachine;

    protected PlayerState(PlayerCtrl playerCtrl, PlayerStateMachine playerStateMachine)
    {
        this.playerCtrl = playerCtrl;
        this.playerStateMachine = playerStateMachine;
    }

    public virtual void Enter() { }
    public virtual void Exit() { }
    public virtual void Tick() { }

}
