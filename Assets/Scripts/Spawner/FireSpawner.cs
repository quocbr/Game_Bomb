using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSpawner : Spawner
{
    private static FireSpawner instance;
    public static FireSpawner Instance => instance;

    public static string fireOne = "Fire1";

    protected override void Awake()
    {
        base.Awake();
        if (FireSpawner.instance != null) Debug.LogError("Only 1 FireSpawner allow to exist");
        FireSpawner.instance = this;
    }

    public virtual List<Transform> Spawn(string prefabName, Vector3 spawnPos, Quaternion rotation)
    {
        Transform prefab = this.GetPrefabByName(prefabName);
        if (prefab == null)
        {
            Debug.LogWarning("Prefab not found: " + prefabName);
            return null;
        }

        List<Vector3> newPos = new List<Vector3>();

            newPos.Add(spawnPos+Vector3.left);
            newPos.Add(spawnPos+Vector3.right);
            newPos.Add(spawnPos+Vector3.down);
            newPos.Add(spawnPos+Vector3.up);


        return this.Spawn(prefab, newPos, rotation);
    }
    
    public virtual List<Transform> Spawn(Transform prefab, List<Vector3> spawnPos, Quaternion rotation)
    {
        List<Transform> Prefab = new List<Transform>();
        foreach (Vector3 x in spawnPos)
        {
            Transform newPrefab = this.GetObjectFromPool(prefab);
            newPrefab.SetPositionAndRotation(x, rotation);
            
            newPrefab.SetParent(this.holder);
            Prefab.Add(newPrefab);
            this.spawnedCount++;
        }
        return Prefab;
    }
}
