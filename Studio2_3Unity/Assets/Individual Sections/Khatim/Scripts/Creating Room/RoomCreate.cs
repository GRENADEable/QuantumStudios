using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomCreate : Photon.PunBehaviour
{
    #region Public Variables
    /*public PhotonLogLevel logLevel = PhotonLogLevel.Informational;
    public byte maxPlayersInRoom = 4;*/
    #endregion

    #region  Private Variables
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

    #region My Functions
    public void CreatingRoom()
    {
        Debug.LogWarning("Room Created");
        RoomOptions roomOption = new RoomOptions()
        {
            IsVisible = true,
            IsOpen = true,
            MaxPlayers = 4
        };
        Debug.LogWarning("Room is Visible, Open and Max Player Set");

        if (PhotonNetwork.CreateRoom(lobby.text, roomOption, TypedLobby.Default))
            Debug.LogWarning("Room Created Succussfully Sent");
        else
            Debug.LogWarning("Room Cration Failed");
    }
    #endregion

    #region Photon Callbacks
    public override void OnPhotonCreateRoomFailed(object[] codeAndMsg)
    {
        Debug.LogWarning("Room Already Exists " + codeAndMsg[1]);
    }

    public override void OnCreatedRoom()
    {
        Debug.LogWarning("Room Created Succussfully");
    }
    #endregion
}
