using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NetworkLobby : Photon.PunBehaviour
{
    #region Public Variables
    public PhotonLogLevel logLevel = PhotonLogLevel.Informational;
    public byte maxPlayersInRoom = 4;
    public TypedLobby newLobby;
    public Text lobbyName;
    private Text lobby
    {
        get
        {
            return lobbyName;
        }
    }
    private string roomName;
    #endregion

    #region  Private Variables
    private string version = "1";
    private bool isConnecting;
    #endregion

    #region Unity Callbacks

    void Awake()
    {
        PhotonNetwork.autoJoinLobby = false;
        //PhotonNetwork.automaticallySyncScene = true;
    }
    void Start()
    {
        isConnecting = true;
        if (PhotonNetwork.connected)
        {
            //PhotonNetwork.JoinRandomRoom();
        }
        else
            PhotonNetwork.ConnectUsingSettings(version);
    }
    #endregion

    #region My Functions
    public void RoomCreate()
    {
        RoomOptions roomOption = new RoomOptions()
        {
            IsVisible = true,
            MaxPlayers = maxPlayersInRoom
        };

        if (PhotonNetwork.CreateRoom(lobby.text, roomOption, TypedLobby.Default))
            Debug.LogWarning("Room Created Successfully");
        else
            Debug.LogWarning("Room Cration Failed");
    }

    /*public void JoinLobby()
    {
        //newLobby = new TypedLobby(/*lobbyName,LobbyType.Default);
        PhotonNetwork.JoinLobby(newLobby);
        PhotonNetwork.JoinLobby(TypedLobby.Default);
    }

    public void JoinRoom(RoomInfo room)
    {
        PhotonNetwork.JoinRoom(room.Name);
    }*/
    #endregion

    #region Photon Callbacks
    public override void OnConnectedToMaster()
    {
        if (isConnecting)
        {
            Debug.LogWarning("Connecting to Master");
            PhotonNetwork.playerName = PlayerNetwork.instance.userName;
            //PhotonNetwork.playerName = PlayerNetwork.instance.userName;
        }
    }

    public override void OnJoinedLobby()
    {
        Debug.LogWarning("Joined Lobby");
        /*Debug.LogWarning("Joined Lobby: " + PhotonNetwork.lobby.Name);
        RoomOptions roomOption = new RoomOptions()
        {
            PublishUserId = true,
            IsVisible = true,
            MaxPlayers = maxPlayersInRoom
        };

        //You can create as many rooms you can. The bigger the number, the lower the chance two players will create a same room.
        roomName = lobbyName + UnityEngine.Random.Range(1, 1024).ToString();
        //Creates the room by the lobby you created.
        PhotonNetwork.CreateRoom(roomName, roomOption, newLobby);
        //PhotonNetwork.JoinRandomRoom();*/
    }

    public override void OnPhotonCreateRoomFailed(object[] codeAndMsg)
    {
        Debug.LogWarning("Room Creation Failed " + codeAndMsg[1]);
    }

    public override void OnCreatedRoom()
    {
        Debug.LogWarning("OnCreatedRoom Created Succussfully");
    }
    #endregion
}
