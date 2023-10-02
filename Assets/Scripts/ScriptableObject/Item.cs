using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Item", menuName = "Item")]
public class Item : ScriptableObject
{
   public new string name;
   public string description;
   public Sprite artwork;

   public int blameRadius;
   public int extraBomb;
   public int speed;
   public int health;
}
