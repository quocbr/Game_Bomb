using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : QuocBehaviour
{
    private static GameManager instance;
    public static GameManager Instance => instance;
    public GameObject[] players;

    protected override void Start()
    {
        PhotonNetwork.Instantiate(players[0].name, new Vector3(-7.5f, 3, 0), Quaternion.identity);
    }

    public void CheckWinState()
    {
        int aliveCount = 0;

        foreach (GameObject player in players)
        {
            if (player.activeSelf) {
                aliveCount++;
            }
        }

        if (aliveCount <= 1) {
            Invoke(nameof(NewRound), 3f);
        }
    }

    private void NewRound()
    {
        PhotonNetwork.LeaveRoom();
        SceneManager.LoadScene("Level_1");
    }
}
