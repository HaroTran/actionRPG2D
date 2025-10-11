using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyCtrl : ObjectCtrl
{
    [SerializeField] protected EnemyStateMachine EnemyStateMachine;
    public EnemyStateMachine StateMachine { get { return EnemyStateMachine; } }
    [SerializeField] protected Animator animator;
    public Animator Animator { get { return animator; } }
    [SerializeField] protected Rigidbody2D rb2d;

    [SerializeField] protected EnemyHealth enemyHealth;
    public EnemyHealth EnemyHealth { get { return enemyHealth; } }
    [SerializeField] protected EnemyDectector enemyDectector;

    public EnemyDectector EnemyDectector { get { return enemyDectector; } }
    [SerializeField] protected EnemyMovement enemyMovement;
    public EnemyMovement EnemyMovement { get { return enemyMovement; } }

    [SerializeField] protected EnemyAttack enemyAttack;
    public EnemyAttack EnemyAttack { get { return enemyAttack; } }
    [SerializeField] protected EnemyStatsSO enemyStatsSO;
    public EnemyStatsSO EnemyStatsSO { get { return enemyStatsSO; } }

    [SerializeField] protected ObjectEffect objectEffect;
    public ObjectEffect ObjectEffect { get { return objectEffect; } }

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
        enemyHealth = GetComponentInChildren<EnemyHealth>();
        enemyDectector = GetComponentInChildren<EnemyDectector>();
        enemyMovement = GetComponentInChildren<EnemyMovement>();
        enemyAttack = GetComponentInChildren<EnemyAttack>();
        EnemyStateMachine = GetComponentInChildren<EnemyStateMachine>();
        objectEffect = GetComponentInChildren<ObjectEffect>();
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

}

