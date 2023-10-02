using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BoomController : MonoBehaviour
{
    private static BoomController instance;
    public static BoomController Instance
    {
        get { return instance; }
    }

    [Header("Explosion")]
    public LayerMask explosionLayerMask;
    public float explosionDuration = 1f;
    public int explosionRadius = 1;
}
