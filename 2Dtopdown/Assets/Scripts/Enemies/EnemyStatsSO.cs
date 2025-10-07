using UnityEngine;

[CreateAssetMenu(fileName = "EnemyStatsSO", menuName = "Scriptable Objects/EnemyStatsSO")]
public class EnemyStatsSO : ScriptableObject
{
    [Header("Attack Stats")]
    [SerializeField] private float attackrange = 1f;
    public float AttackRange { get { return attackrange; } }
    [SerializeField] private float attackDamage = 10f;
    public float AttackDamage { get { return attackDamage; } }
    [Header("Detect Stats")]
    [SerializeField] private float detectRadius = 5f;
    public float DetectRadius { get { return detectRadius; } }

    [Header("Move Stats")]
    [SerializeField] private float moveSpeed = 2f;
    public float MoveSpeed { get { return moveSpeed; } }
}
