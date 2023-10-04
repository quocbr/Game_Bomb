using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ConnecToSeverController : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.NickName = MasterManager.GameSetting.NickName;
        PhotonNetwork.GameVersion = MasterManager.GameSetting.GameVersiron;
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connented to master");
        PhotonNetwork.JoinLobby();
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("Disconnected to server");
    }

    public override void OnJoinedLobby()
    {
        //SceneManager.LoadScene("Lobby");
    }
}
