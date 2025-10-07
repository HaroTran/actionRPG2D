using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyCtrl : ObjectCtrl
{
    [SerializeField]private EnemyState currentState = EnemyState.Idle;  
    public EnemyState CurrentState { get { return currentState; } set { currentState = value; } }
    [SerializeField] protected Animator animator;
    public Animator Animator { get { return animator; } }
    [SerializeField] protected Rigidbody2D rb2d;
    [SerializeField] protected EnemyDectector enemyDectector;
    public EnemyDectector EnemyDectector { get { return enemyDectector; } }
    [SerializeField] protected EnemyMovement enemyMovement;
    public EnemyMovement EnemyMovement { get { return enemyMovement; } }
    [SerializeField] protected EnemyStatsSO enemyStatsSO;
    public EnemyStatsSO EnemyStatsSO { get { return enemyStatsSO; } }

    protected override void Reset()
    {
        ResetAllComponents();
    }
    protected override void Awake()
    {
        ResetAllComponents();
    }

    protected override void ResetAllComponents()
    {
        enemyDectector = GetComponentInChildren<EnemyDectector>();
        enemyMovement = GetComponentInChildren<EnemyMovement>();
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

}
public enum EnemyState
{
    Idle,
    Chase,
    Move,
    Attack,
    Hit,
    Die
}