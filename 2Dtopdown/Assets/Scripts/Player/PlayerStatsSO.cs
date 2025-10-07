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
    
}
