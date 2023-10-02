using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tesst : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, Vector2.one/2f) ;
    }
}
