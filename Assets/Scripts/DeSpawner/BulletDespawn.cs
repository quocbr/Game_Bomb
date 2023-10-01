using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDespawn : DespawnByTime
{
    public override void DespawnObject()
    {
        BulletSpawner.Instance.Despawn(transform.parent);
    }

    public void InitFireObject()
    {
        List<Transform> newBullet1 = FireSpawner.Instance.Spawn(FireSpawner.fireOne,transform.position, transform.rotation);
        foreach (Transform transform1 in newBullet1)
        {
            transform1.gameObject.SetActive(true);
        }
        
    }

}
