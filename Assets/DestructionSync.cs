using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapSync : MonoBehaviourPunCallbacks,IPunObservable
{
    private Tilemap tilemap;
    private Dictionary<Vector3Int, TileBase> synchronizedTiles = new Dictionary<Vector3Int, TileBase>();

    private void Start()
    {
        tilemap = GetComponent<Tilemap>();
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // Ghi dữ liệu đồng bộ hóa vào stream (chế độ ghi)
            foreach (var kvp in synchronizedTiles)
            {
                stream.SendNext(kvp.Key);
                stream.SendNext(kvp.Value.name);
            }
        }
        else
        {
            // Đọc dữ liệu đồng bộ hóa từ stream (chế độ đọc)
            synchronizedTiles.Clear();

            while (stream.PeekNext() != null)
            {
                Vector3Int cellPosition = (Vector3Int)stream.ReceiveNext();
                string tileName = (string)stream.ReceiveNext();

                TileBase tile = ScriptableObject.CreateInstance<TileBase>();
                tile.name = tileName;

                synchronizedTiles[cellPosition] = tile;
            }
        }
    }

    private void ApplySyncedTiles()
    {
        // Áp dụng các thay đổi từ synchronizedTiles lên tilemap
        foreach (var kvp in synchronizedTiles)
        {
            tilemap.SetTile(kvp.Key, kvp.Value);
        }
    }
}