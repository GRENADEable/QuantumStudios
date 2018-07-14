using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lobby : Photon.PunBehaviour
{
    #region  Private Variables
    private string version = "1";
    #endregion

    #region Unity Callbacks
    void Awake()
    {
        Debug.LogWarning("Connecting to Server");
        PhotonNetwork.ConnectUsingSettings(version);
    }
    #endregion

    #region Photon Callbacks
    public override void OnConnectedToMaster()
    {
        Debug.LogWarning("Connected to Master");
        //PhotonNetwork.playerName = PlayerNetwork.instance.userName;

        PhotonNetwork.JoinLobby(TypedLobby.Default);
    }

    public override void OnJoinedLobby()
    {
        Debug.LogWarning("Joined Lobby");
    }
    #endregion
}
