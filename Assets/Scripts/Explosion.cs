using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public GameObject start;
    public GameObject middle;
    public GameObject end;

    public void SetActiveRenderer(string renderer)
    {
        start.SetActive(renderer == start.name);
        middle.SetActive(renderer == middle.name);
        end.SetActive(renderer == end.name);
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
