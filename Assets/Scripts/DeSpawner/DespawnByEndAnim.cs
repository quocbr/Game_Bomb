using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnByEndAnim : Despawn
{
    private bool checkEndAnim = false;
    protected override bool CanDespawn()
    {
        return checkEndAnim;
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        checkEndAnim = false;
        
    }

    public void setcheckAnim()
    {
        checkEndAnim = true;
    }
}
