using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireDespawn : DespawnByTime
{
    public override void DespawnObject()
    {
        FireSpawner.Instance.Despawn(transform.parent);
    }
}
