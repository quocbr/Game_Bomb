using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDisplay : MonoBehaviour
{
    public Item item;
    
    public int blameRadius;
    public int extraBomb;
    public int speed;
    public int health;

    private void Start()
    {
        this.blameRadius = item.blameRadius;
        this.extraBomb = item.extraBomb;
        this.speed = item.speed;
        this.health = item.health;
    }
}
