using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public GameObject start;
    public GameObject middle;
    public GameObject end;
    
    public void SetActiveRenderer(GameObject renderer)
    {
        start.SetActive(renderer == start);
        middle.SetActive(renderer == middle);
        end.SetActive(renderer == end);
    }
    public void SetDirection(Vector2 direction)
    {
        float angle = Mathf.Atan2(direction.y, direction.x);
        transform.rotation = Quaternion.AngleAxis(angle * Mathf.Rad2Deg, Vector3.forward);
    }

    public void DestroyAfter(float seconds)
    {
        Destroy(gameObject, seconds);
    }
}
