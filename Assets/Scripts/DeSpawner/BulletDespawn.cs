using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDespawn : DespawnByTime
{
    public Explosion explosionPrefab;
    public override void DespawnObject()
    {
        BulletSpawner.Instance.Despawn(transform.parent);
    }

    public void InitFireObject()
    {
        // List<Transform> newBullet1 = FireSpawner.Instance.Spawn(FireSpawner.fireOne,transform.position, transform.rotation);
        // foreach (Transform transform1 in newBullet1)
        // {
        //     transform1.gameObject.SetActive(true);
        // }
        
        Vector3 position = this.transform.position;

        Explosion explosion = Instantiate(explosionPrefab, position, Quaternion.identity);
        explosion.SetActiveRenderer(explosion.start);
        explosion.DestroyAfter(BoomController.Instance.explosionDuration);

        Explode(position, Vector2.up, BoomController.Instance.explosionRadius);
        Explode(position, Vector2.down, BoomController.Instance.explosionRadius);
        Explode(position, Vector2.left, BoomController.Instance.explosionRadius);
        Explode(position, Vector2.right, BoomController.Instance.explosionRadius);

        
    }
    
    private void Explode(Vector2 position, Vector2 direction, int length)
    {
        if (length <= 0) {
            return;
        }

        position += direction;

        if (Physics2D.OverlapBox(position, Vector2.one / 2f, 0f, BoomController.Instance.explosionLayerMask))
        {
            //ClearDestructible(position);
            return;
        }
        Explosion explosion = Instantiate(explosionPrefab, position, Quaternion.identity);
        explosion.SetActiveRenderer(length > 1 ? explosion.middle : explosion.end);
        explosion.SetDirection(direction);
        explosion.DestroyAfter(BoomController.Instance.explosionDuration);

        Explode(position, direction, length - 1);
    }

}
