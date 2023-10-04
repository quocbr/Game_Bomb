using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class Destructible : MonoBehaviourPunCallbacks
{
    public float destructionTime = 1f;

    [Range(0f, 1f)]
    public float itemSpawnChance = 0.2f;
    public GameObject[] spawnableItems;

    private void Start()
    {
        Destroy(gameObject, destructionTime);
    }

    private void OnDestroy()
    {
        if (spawnableItems.Length > 0 && Random.value < itemSpawnChance)
        {
            int randomIndex = Random.Range(0, spawnableItems.Length);
            PhotonNetwork.Instantiate(spawnableItems[randomIndex].name, transform.position, Quaternion.identity);
        }
    }
}
