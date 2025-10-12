using Unity.AppUI.UI;
using UnityEngine;

[CreateAssetMenu(fileName = "ProjectileStatSO", menuName = "Scriptable Objects/ProjectileStatSO")]
public class ProjectileStatSO : ScriptableObject
{
    [SerializeField] protected int damage=3;
    public int Damage { get { return damage; } }

    [SerializeField] protected float speed=15f;
    public float Speed { get { return speed; } }

    [SerializeField] protected float lifetime=3f;
    public float Lifetime { get { return lifetime; } }

    [SerializeField] protected LayerMask hitlayer;
    public LayerMask HitLayer { get { return hitlayer; } }
}
