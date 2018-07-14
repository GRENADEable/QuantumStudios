using System.Collections.Generic;
using UnityEngine;

public class GroupedRoomLayout : Photon.PunBehaviour
{
    #region Public Variables
    public GameObject roomListingPrefab
    {
        get
        {
            return _roomListingPrefab;
        }
    }
    #endregion

    #region  Private Variables
    [SerializeField]
    private GameObject _roomListingPrefab;

    [SerializeField]
    private List<ListOfRoom> _roomListingButtons = new List<ListOfRoom>();
    private List<ListOfRoom> roomListingButtons
    {
        get
        {
            return _roomListingButtons;
        }
    }
    #endregion

    #region My Functions
    void RoomRecieved(RoomInfo room)
    {
        //Checks the scripts in the Room Butttons. Each list will be an entery
        //called X and compare the string roomName with the name of the room.
        int index = roomListingButtons.FindIndex(x => x.roomName == room.Name);
        Debug.LogWarning("Index Being Compared");
        if (index == -1)
        {
            if (room.IsVisible && room.PlayerCount < room.MaxPlayers)
            {
                Debug.LogWarning("Passed if Condition");
                GameObject roomListingObj = Instantiate(roomListingPrefab);
                roomListingObj.transform.SetParent(transform, false);
                Debug.LogWarning("roomListingObj instantiated and Set to Parnet");

                ListOfRoom roomListing = roomListingObj.GetComponent<ListOfRoom>();
                roomListingButtons.Add(roomListing);
                Debug.LogWarning("Getting Room Script from each Room Listing Prefab.");
                Debug.LogWarning("Adding that Script to the List");

                index = (roomListingButtons.Count - 1);
                Debug.LogWarning("Index Updated");
            }
        }

        if (index != -1)
        {
            ListOfRoom roomListing = roomListingButtons[index];
            roomListing.SetRoom(room.Name);
            Debug.LogWarning("Room Name Set form List");
            roomListing.isUpdated = true;
            Debug.LogWarning("isUpdated Set to True");
        }
    }

    void RemoveOldRooms()
    {
        List<ListOfRoom> removeRooms = new List<ListOfRoom>();

        foreach (ListOfRoom roomListing in roomListingButtons)
        {
            if (roomListing.isUpdated)
                removeRooms.Add(roomListing);
            else
                roomListing.isUpdated = false;
        }
        Debug.LogWarning("Server Removed");

        foreach (ListOfRoom roomListing in removeRooms)
        {
            GameObject roomListingObj = roomListing.gameObject;
            roomListingButtons.Remove(roomListing);
            Destroy(roomListingObj);
            Debug.LogWarning("Server Destroyed");
        }

    }
    #endregion

    #region Photon Callbacks
    public override void OnReceivedRoomListUpdate()
    {
        RoomInfo[] rooms = PhotonNetwork.GetRoomList();
        Debug.LogWarning("Gets Array of RoomInfo");

        foreach (RoomInfo room in rooms)
        {
            RoomRecieved(room);
        }

        RemoveOldRooms();
        Debug.LogWarning("RemoveOldRooms Function Called");
    }
    #endregion
}
