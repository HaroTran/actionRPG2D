using UnityEngine;

public class VFX_Spawner : Spawner
{
    public static VFX_Spawner Instance { get; private set; }

    protected override void ResetAllComponents()
    {
        base.ResetAllComponents();
        Instance = this;
    }
    protected override void Awake()
    {
        base.Awake();
        if (VFX_Spawner.Instance != null)
        {
            Debug.Log("only one instance of VFX_Spawner allowed");
            VFX_Spawner.Instance = this;
        }
    }
    public virtual Transform Spawn(VFX_name prefabName, Vector2 spawnpos, Quaternion rotation)
    {
        Transform newPrefab = GetObjectFromPool(prefabName);
        if (newPrefab == null) return null;

        newPrefab.SetPositionAndRotation(spawnpos, rotation);
        newPrefab.gameObject.SetActive(true);
        newPrefab.parent=this.holder;   
        return newPrefab;
    }

    protected Transform GetObjectFromPool(VFX_name prefabName)
    {
        foreach (Transform prefab in this.poolObjs)
        {
            if (prefab.GetComponent<VFXCtrl>().VFX_name.ToString() == prefabName.ToString())
            {
                this.poolObjs.Remove(prefab);
                return prefab;
            }
        }
        Debug.Log("Not found prefab: " + prefabName + " in pool");
        Debug.Log("Spawner create an instance of prefab");
        foreach (Transform prefab in this.prefabs)
        {
            if (prefab.GetComponent<VFXCtrl>().VFX_name.ToString() == prefabName.ToString())
            {
                return Instantiate(prefab);
            }
        }
        return null;
    }

}
public enum VFX_name
{
    Soul,
}
