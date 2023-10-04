using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

public class CreateAndJoinRoom : MonoBehaviourPunCallbacks
{
    public InputField roomNameCreateInputField;
    public InputField roomNameJoinInputField;

    public void CreateRoom()
    {
        Debug.Log("Create room "+ roomNameCreateInputField.text);
        PhotonNetwork.CreateRoom(roomNameCreateInputField.text,new RoomOptions(){MaxPlayers = 4,IsVisible = true,IsOpen = true},TypedLobby.Default,null);
    }
    
    public void JoinRoom()
    {
        Debug.Log("Joinroom "+ roomNameJoinInputField.text);
        PhotonNetwork.JoinRoom(roomNameJoinInputField.text);
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("SampleScene");
    }

    public override void OnCreatedRoom()
    {

    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        base.OnCreateRoomFailed(returnCode, message);
    }
}
