using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class PlayerListingMenu : MonoBehaviourPunCallbacks
{
    [SerializeField] private Transform _content;
    [SerializeField] private PlayerListing _playerListing;
    private List<PlayerListing> _listings = new List<PlayerListing>();

    private RoomsCanvas _roomsCanvas;

    private void Awake()
    {
        GetCurrentRoomPlayer();
    }

    public void FirstInitialize(RoomsCanvas canvas)
    {
        _roomsCanvas = canvas;
    }

    public override void OnLeftRoom()
    {
        _content.DestroyChildren();
    }

    private void GetCurrentRoomPlayer()
    {
        foreach (KeyValuePair<int,Player> playerInfo in PhotonNetwork.CurrentRoom.Players)
        {
            AddPlayerListing(playerInfo.Value);
        }
        
    }

    private void AddPlayerListing(Player player)
    {
        PlayerListing listing = Instantiate(_playerListing, _content);

        if (listing != null)
        {
            listing.SetPlayerInfo(player);
            _listings.Add(listing);
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        AddPlayerListing(newPlayer);
    }
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        int index = _listings.FindIndex(x => x.Player == otherPlayer);

        if (index != -1)
        {
            Destroy(_listings[index].gameObject);
            _listings.RemoveAt(index);
        }
    }
}
