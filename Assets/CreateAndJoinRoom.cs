using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class CreateAndJoinRoom : MonoBehaviourPunCallbacks
{
    public InputField roomNameCreateInputField;
    public InputField roomNameJoinInputField;

    public void CreateRoom()
    {
        Debug.Log("Create room "+ roomNameCreateInputField.text);
        PhotonNetwork.CreateRoom(roomNameCreateInputField.text);
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
}
