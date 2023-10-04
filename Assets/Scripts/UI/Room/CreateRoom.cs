using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

public class CreateRoom : MonoBehaviourPunCallbacks
{
    [SerializeField] private Text _roomName;

    private RoomsCanvas _roomsCanvas;
    
    public void FirstInitialize(RoomsCanvas canvas)
    {
        _roomsCanvas = canvas;
    }
    
    public void OnClick_CreateRoom()
    {
        if (!PhotonNetwork.IsConnected)
        {
            return;
        }
        PhotonNetwork.CreateRoom(_roomName.text,new RoomOptions(){MaxPlayers = 4,IsVisible = true,IsOpen = true},TypedLobby.Default,null);
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("Create room succees",this);
        _roomsCanvas.CurrenRoomCanvas.Show();
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Create room Fail"+message,this);
    }
}
