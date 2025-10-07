using UnityEngine;

public class HaroMonoBehavior : MonoBehaviour
{
    protected virtual void Reset()
    {
        ResetAllComponents();
    }
    protected virtual void Awake()
    {
        ResetAllComponents();
    }

    protected virtual void ResetAllComponents()
    {

    }
}
