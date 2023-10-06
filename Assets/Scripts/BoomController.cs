using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BoomController : MonoBehaviourPunCallbacks
{
    private static BoomController instance;
    public static BoomController Instance => instance;

    [Header("Bomb")] private PlayerInputAction _playerInputAction;
    public GameObject bombPrefab;
    public float bombFuseTime = 3f;
    public int bombAmount = 1;
    private int bombsRemaining;
    public int getbombsRemaining => bombsRemaining;

    [Header("Explosion")] public Explosion explosionPrefab;
    public LayerMask explosionLayerMask;
    public float explosionDuration = 1f;
    public int explosionRadius = 1;

    [Header("Destructible")] public Tilemap destructibleTiles;
    public Destructible destructiblePrefab;

    public override void OnEnable()
    {
        bombsRemaining = bombAmount;
        _playerInputAction.Enable();
    }

    public override void OnDisable()
    {
        _playerInputAction.Disable();
    }

    protected virtual void Awake()
    {
        _playerInputAction = new PlayerInputAction();
        instance = this;
        LoadComponents();
    }

    protected virtual void LoadComponents()
    {
        destructibleTiles = GameObject.FindGameObjectWithTag("Destruction").GetComponent<Tilemap>();
    }

    private void Update()
    {
        if (photonView.IsMine)
        {
            if (bombsRemaining > 0 && _playerInputAction.Player.Fire.triggered)
            {
                StartCoroutine(PlaceBomb());
            }
        }
    }

    public IEnumerator PlaceBomb()
    {
        Vector2 position = transform.position;
        position.x = (int) position.x + (position.x > 0.0 ? 0.5f : -0.5f);
        position.y = (int) (position.y + (position.y > 0.0 ? 0.5f : -0.5f));

        //GameObject bomb = Instantiate(bombPrefab, position, Quaternion.identity);
        GameObject bomb = PhotonNetwork.Instantiate(bombPrefab.name, position, Quaternion.identity);
        bombsRemaining--;

        yield return new WaitForSeconds(bombFuseTime);

        position = bomb.transform.position;
        position.y = position.y + 0.5f;

        //Explosion explosion = Instantiate(explosionPrefab, position, Quaternion.identity);
        GameObject explosionGameObject = PhotonNetwork.Instantiate(explosionPrefab.name, position, Quaternion.identity);
        explosionGameObject.gameObject.GetComponent<Explosion>().SetActiveRenderer("Start");

        //explosion.DestroyAfter(explosionDuration);
        Destroy(explosionGameObject,explosionDuration);

        Explode(position, Vector2.up, explosionRadius);
        Explode(position, Vector2.down, explosionRadius);
        Explode(position, Vector2.left, explosionRadius);
        Explode(position, Vector2.right, explosionRadius);

        PhotonNetwork.Destroy(bomb.gameObject);
        bombsRemaining++;
    }

    private void Explode(Vector2 position, Vector2 direction, int length)
    {
        if (length <= 0)
        {
            return;
        }

        position += direction;

        if (Physics2D.OverlapBox(position, Vector2.one / 2f, 0f, explosionLayerMask))
        {
            ClearDestructible(position);
            return;
        }

        //Explosion explosion = PhotonNetwork.Instantiate(explosionPrefab, position, Quaternion.identity);
        GameObject explosionGameObject = PhotonNetwork.Instantiate(explosionPrefab.name, position, Quaternion.identity);

        this.SetActiveRenderer(explosionGameObject.GetComponent<Transform>(),length > 1 ? "Middle" : "End");
        this.SetDirection(explosionGameObject,direction);
        Destroy(explosionGameObject,explosionDuration);


        Explode(position, direction, length - 1);
    }
    
    public void SetActiveRenderer(Transform explor, string renderer)
    {
        foreach (Transform c in explor)
        {
            GameObject childObject = c.gameObject;
            childObject.SetActive(renderer == childObject.name);
        }
    }
    
    public void SetDirection(GameObject explor,Vector2 direction)
    {
        float angle = Mathf.Atan2(direction.y, direction.x);
        explor.transform.rotation = Quaternion.AngleAxis(angle * Mathf.Rad2Deg, Vector3.forward);
    }

    private void ClearDestructible(Vector2 position)
    {
        Vector3Int cell = destructibleTiles.WorldToCell(position);
        TileBase tile = destructibleTiles.GetTile(cell);

        if (tile != null)
        {
            //Instantiate(destructiblePrefab, position, Quaternion.identity);
            PhotonNetwork.Instantiate(destructiblePrefab.name, position, Quaternion.identity);
            //destructibleTiles.SetTile(cell, null);
            OnTilemapChanged(cell);
        }
    }
    
    // Sự kiện được gọi khi có thay đổi trên tilemap
    private void OnTilemapChanged(Vector3Int position)
    {
        if (photonView.IsMine)
        {
            // Gửi thông tin thay đổi thông qua Photon để đồng bộ hóa với người chơi khác
            photonView.RPC("SyncTilemapChange", RpcTarget.All, position);
        }
    }
    
    [PunRPC]
    private void SyncTilemapChange(Vector3Int position)
    {
        // Áp dụng thông tin thay đổi từ người chơi khác lên tilemap
        TileBase newTile = destructibleTiles.GetTile(position);
        destructibleTiles.SetTile(position, null);
        destructibleTiles.RefreshTile(position);
    }

    public void AddBomb()
    {
        bombAmount++;
        bombsRemaining++;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Bomb"))
        {
            other.isTrigger = false;
        }
    }
}
