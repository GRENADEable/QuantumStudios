using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CreateLobby : Photon.PunBehaviour
{
    #region Public Variables
    #endregion

    #region  Private Variables
    [SerializeField]
    private Text _roomName;
    private Text roomName
    {
        get { return _roomName; }
    }
    #endregion

    #region My Functions
    public void CreateRoom()
    {
        if (PhotonNetwork.CreateRoom(roomName.text))
            Debug.LogWarning("Room Created Succussfully");
        else
            Debug.LogWarning("Room Failed to Create");
    }
    #endregion
    #region Unity Callbacks
    #endregion

    #region Photon Callbacks
    public override void OnPhotonCreateRoomFailed(object[] codeAndMsg)
    {
        Debug.LogWarning("Room Failed to Create: " + codeAndMsg[1]);
    }
    public override void OnCreatedRoom()
    {
        Debug.LogWarning("Room Created Succussfully");
    }
    #endregion
}
