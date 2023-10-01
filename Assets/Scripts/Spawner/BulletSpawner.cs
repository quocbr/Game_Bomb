using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : Spawner
{
    private static BulletSpawner instance;
    public static BulletSpawner Instance => instance;

    public static string bulletOne = "Boom1";

    protected override void Awake()
    {
        base.Awake();
        if (BulletSpawner.instance != null) Debug.LogError("Only 1 BulletSpawner allow to exist");
        BulletSpawner.instance = this;
    }

    public override void Despawn(Transform obj)
    {
        base.Despawn(obj);
        obj.GetChild(0).GetComponent<Collider2D>().isTrigger = true;
    }
}
