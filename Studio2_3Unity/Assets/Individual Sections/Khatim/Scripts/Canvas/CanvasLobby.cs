using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasLobby : MonoBehaviour
{
    #region  Private Variables
    [SerializeField]
    private GroupedRoomLayout _groupedRoomLayout;
    private GroupedRoomLayout groupedRoomLayout
    {
        get
        {
            return _groupedRoomLayout;
        }
    }
    #endregion

    #region My Functions
    public void OnClickJoinRoom(string room)
    {
         
    }
    #endregion

    #region Photon Callbacks

    #endregion
}
