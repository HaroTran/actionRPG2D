using Unity.VisualScripting;
using UnityEngine;

public class ProjectileSpawner : Spawner
{
    public static ProjectileSpawner Instance { get; private set; }

    protected override void ResetAllComponents()
    {
        base.ResetAllComponents();
        Instance = this;
    }
    protected override void Awake()
    {
        base.Awake();
        if(ProjectileSpawner.Instance!=null)
        {
            Debug.Log("only one instance of ProjectileSpawner allowed");
            ProjectileSpawner.Instance = this;
        }
    }

    public virtual Transform Spawn(string prefabName, Vector2 spawnpos, Quaternion rotation)
    {
        Transform newPrefab = GetObjectFromPool(prefabName);
        if (newPrefab == null) return null;

        newPrefab.SetPositionAndRotation(spawnpos, rotation);
        newPrefab.gameObject.SetActive(true);
        newPrefab.parent=this.holder;
        return newPrefab;
    }

    protected Transform GetObjectFromPool(string prefabName)
    {
        foreach (Transform prefab in this.poolObjs)
        {
            if (prefab.name == prefabName)
            {
                this.poolObjs.Remove(prefab);
                return prefab;
            }
        }
        Debug.Log("Not found prefab: " + prefabName + " in pool");
        Debug.Log("Spawner create an instance of prefab");
        foreach (Transform prefab in this.prefabs)
        {
            if (prefab.name == prefabName)
            {
                return Instantiate(prefab);
            }
        }
        return null;
    }
}
