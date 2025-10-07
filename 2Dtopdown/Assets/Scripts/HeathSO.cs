using UnityEngine;

[CreateAssetMenu(fileName = "HeathSO", menuName = "Scriptable Objects/HeathSO")]
public class HeathSO : ScriptableObject
{
    [SerializeField] int maxHealth;
    public int MaxHealth { get { return maxHealth; } }
}
