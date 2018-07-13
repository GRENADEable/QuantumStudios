using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NetworkLobby : Photon.PunBehaviour
{
    #region Public Variables
    public PhotonLogLevel logLevel = PhotonLogLevel.Informational;
    public byte maxPlayersInRoom = 4;
    //public GameObject controlPanel;
    //public GameObject progressLabel;
    private string roomName;
    public TypedLobby newLobby;
    #endregion

    #region  Private Variables
    private string version = "1";
    [SerializeField]
    private Text _lobbyName;
    private bool isConnecting;
    private Text lobbyName
    {
        get { return _lobbyName; }
    }
    #endregion

    #region Unity Callbacks
    void Start()
    {
        /*Debug.LogWarning("Connecting to Server");
        PhotonNetwork.ConnectUsingSettings(version);*/
    }
    #endregion

    #region Photon Callbacks
    public override void OnConnectedToMaster()
    {
        if (isConnecting)
        {
            Debug.LogWarning("Connecting to Master");
            //PhotonNetwork.playerName = PlayerNetwork.instance.userName;
            JoinLobby();
        }
    }

    public override void OnJoinedLobby()
    {
        Debug.LogWarning("Joined Lobby");
        Debug.LogWarning("In Lobby: " + PhotonNetwork.lobby.Name);
        RoomOptions roomOption = new RoomOptions()
        {
            PublishUserId = true,
            IsVisible = true,
            MaxPlayers = maxPlayersInRoom
        };

        //You can create as many rooms you can. The bigger the number, the lower the chance two players will create a same room.
        roomName = _lobbyName + UnityEngine.Random.Range(1, 1024).ToString();
        //Creates the room by the lobby you created.
        PhotonNetwork.CreateRoom(roomName, roomOption, newLobby);
        //PhotonNetwork.JoinRandomRoom();*/
    }
    #endregion

    #region My Functions
    public void ConnectToServer()
    {
        isConnecting = true;
        if (PhotonNetwork.connected)
        {
            PhotonNetwork.JoinRandomRoom();
        }
        else
            PhotonNetwork.ConnectUsingSettings(version);
    }
    public void JoinLobby()
    {
        newLobby = new TypedLobby(_lobbyName.ToString(), LobbyType.Default);
        PhotonNetwork.JoinLobby(newLobby);
    }

    public void JoinRoom(RoomInfo room)
    {
        PhotonNetwork.JoinRoom(room.Name);
    }
    #endregion
}
