using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStatsSO", menuName = "Scriptable Objects/PlayerStatsSO")]
public class PlayerStatsSO : ScriptableObject
{
    [Header("Health Stats")]
    [SerializeField] private int maxHealth;
    public int MaxHealth { get { return maxHealth; } }

    [Header("Movement Stats")]
    [SerializeField] private float moveSpeed;
    public float MoveSpeed { get { return moveSpeed; } }

    [Header("Attack Stats")]
    [SerializeField] private int attackDamage;
    public int AttackDamage { get { return attackDamage; } }
    [SerializeField] private float attackRange;
    public float AttackRange { get { return attackRange; } }
    [SerializeField] private float attackCooldown;
    public float AttackCooldown { get { return attackCooldown; } }
    
}
