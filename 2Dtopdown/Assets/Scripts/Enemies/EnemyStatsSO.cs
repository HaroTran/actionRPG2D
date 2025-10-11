using UnityEngine;

[CreateAssetMenu(fileName = "EnemyStatsSO", menuName = "Scriptable Objects/EnemyStatsSO")]
public class EnemyStatsSO : ScriptableObject
{
    [Header("Health Stats")]
    [SerializeField] private int maxHealth = 100;
    public int MaxHealth { get { return maxHealth; } }
    
    [Header("Attack Stats")]
    [SerializeField] private float attackrange = 1f;
    public float AttackRange { get { return attackrange; } }
    [SerializeField] private int attackDamage = 10;
    public int AttackDamage { get { return attackDamage; } }
    [SerializeField] private float attackCooldown = 3f;
    public float AttackCooldown { get { return attackCooldown; } }
    [Header("Detect Stats")]
    [SerializeField] private float detectRadius = 5f;
    public float DetectRadius { get { return detectRadius; } }

    [Header("Move Stats")]
    [SerializeField] private float moveSpeed = 2f;
    public float MoveSpeed { get { return moveSpeed; } }
}
