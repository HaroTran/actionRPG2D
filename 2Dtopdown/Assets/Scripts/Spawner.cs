using System.Collections.Generic;
using UnityEngine;

public class Spawner : HaroMonoBehavior
{
    [Header("Spawner")]

    [SerializeField] protected Transform holder;
    [SerializeField] protected List<Transform> prefabs;
    [SerializeField] protected List<Transform> poolObjs;


    protected override void ResetAllComponents()
    {
        base.ResetAllComponents();
        LoadHolder();
        LoadPrefabs();
    }

    protected override void Awake()
    {
        base.Awake();
        ResetAllComponents();

    }
    protected virtual void LoadHolder()
    {
        if (this.holder != null) return;

        this.holder = transform.Find("Holder");
    }
    protected virtual void LoadPrefabs()
    {
        if (this.prefabs.Count>0)
        {
            //Debug.Log("Prefabs already loaded");
            return;
        } 

        Transform prefabObjs = transform.Find("Prefabs");
        foreach (Transform prefab in prefabObjs)
        {
            this.prefabs.Add(prefab);
        }

        this.HidePrefabs();
    }

    protected virtual void HidePrefabs()
    {
        if (this.prefabs == null) return;

        foreach (Transform prefab in this.prefabs)
        {
            prefab.gameObject.SetActive(false);
            this.poolObjs.Add(prefab);
        }
    }

    public virtual void Despawn(Transform obj)
    {
        this.poolObjs.Add(obj);
        obj.gameObject.SetActive(false);
    }

    protected virtual Transform GetObjectFromPool(Transform prefab)
    {
        foreach (Transform poolObj in this.poolObjs)
        {
            if (poolObj.name == prefab.name)
            {
                this.poolObjs.Remove(poolObj);
                return poolObj;
            }
        }

        Transform newPrefab = Instantiate(prefab);
        newPrefab.name = prefab.name;
        return newPrefab;
    }
}
