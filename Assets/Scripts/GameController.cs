using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private GameController _instance;
    public GameController Instance => _instance;

    private void Awake()
    {
        _instance = this;
    }
}
