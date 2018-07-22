using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LobbyManager : Photon.PunBehaviour
{
    #region  Public Variables
    public InputField roomName;
    public GameObject roomButtonPrefab;
    public PhotonLogLevel logLevel = PhotonLogLevel.Informational;
    #endregion

    #region  Private Variables
    //private static LobbyManager instance;
    private string version = "1";
    private List<GameObject> roomButtonPrefabs = new List<GameObject>();
    private bool joinedLobby;
    //private bool isConnecting;
    #endregion

    #region Unity Callbacks
    void Awake()
    {
        PhotonNetwork.autoJoinLobby = false;
        PhotonNetwork.automaticallySyncScene = false;

        //PhotonNetwork.sendRate = 60; //Default 20
        //PhotonNetwork.sendRateOnSerialize = 30; //Default 10
    }

    void Start()
    {
        PhotonNetwork.ConnectUsingSettings(version);
        joinedLobby = false;
    }
    #endregion

    #region My Functions
    public void TwoPlayerRoom()
    {
        //isConnecting = true;
        if (PhotonNetwork.JoinLobby())
        {
            Debug.LogWarning("Creating Room");
            RoomOptions roomOpt = new RoomOptions()
            {
                IsVisible = true,
                IsOpen = true,
                MaxPlayers = 2
            };

            PhotonNetwork.CreateRoom(roomName.text, roomOpt, TypedLobby.Default);
            Debug.LogWarning("Created Room with Lobby Name " + roomName.text);
        }
    }

    public void FourPlayerRoom()
    {
        //isConnecting = true;
        if (PhotonNetwork.JoinLobby())
        {
            Debug.LogWarning("Creating Room");
            RoomOptions roomOpt = new RoomOptions()
            {
                IsVisible = true,
                IsOpen = true,
                MaxPlayers = 4
            };

            PhotonNetwork.CreateRoom(roomName.text, roomOpt, TypedLobby.Default);
            Debug.LogWarning("Created Room with Lobby Name " + roomName.text);
        }
    }

    public void RefreshRoom()
    {
        if (joinedLobby)
        {
            if (roomButtonPrefabs.Count > 0)
            {
                for (int i = 0; i < roomButtonPrefabs.Count; i++)
                {
                    Destroy(roomButtonPrefabs[i]);
                }
            }
            roomButtonPrefabs.Clear();
        }

        for (int i = 0; i < PhotonNetwork.GetRoomList().Length; i++)
        {
            Debug.LogWarning(PhotonNetwork.GetRoomList()[i].Name);
            GameObject room = Instantiate(roomButtonPrefab);
            room.transform.SetParent(roomButtonPrefab.transform.parent);

            room.GetComponent<RectTransform>().localScale = roomButtonPrefab.GetComponent<RectTransform>().localScale;
            room.GetComponent<RectTransform>().localPosition = new Vector3(roomButtonPrefab.GetComponent<RectTransform>().localPosition.x, roomButtonPrefab.GetComponent<RectTransform>().localPosition.y - (i * 50), roomButtonPrefab.GetComponent<RectTransform>().localPosition.z);
            room.gameObject.transform.Find("RoomNameText").GetComponent<Text>().text = PhotonNetwork.GetRoomList()[i].Name;
            room.gameObject.transform.Find("PlayerCountText").GetComponent<Text>().text = PhotonNetwork.GetRoomList()[i].PlayerCount + "/" + PhotonNetwork.GetRoomList()[i].MaxPlayers;
            room.gameObject.transform.Find("JoinButton").GetComponent<Button>().onClick.AddListener(() => { JoinRoom(room.gameObject.transform.Find("RoomNameText").GetComponent<Text>().text); });

            room.SetActive(true);
            roomButtonPrefabs.Add(room);

        }
    }

    void JoinRoom(string room)
    {
        bool roomVisible = false;

        foreach (RoomInfo info in PhotonNetwork.GetRoomList())
        {
            if (room == info.Name)
            {
                roomVisible = true;
                break;
            }
            else
                roomVisible = false;
        }

        if (roomVisible)
            PhotonNetwork.JoinRoom(room);
        else
            Debug.LogWarning("Room Not Found");
    }

    public void RandomRoom()
    {
        if (PhotonNetwork.GetRoomList().Length > 0)
            PhotonNetwork.JoinRandomRoom();
        else
            Debug.LogWarning("No Random Room Found");
    }
    #endregion

    #region Photon Callbacks
    public override void OnConnectedToMaster()
    {
        Debug.LogWarning("Connectng to Master");
        PhotonNetwork.JoinLobby();
    }

    void OnGUI()
    {
        GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
    }
    public override void OnJoinedLobby()
    {
        Debug.LogWarning("Joined Lobby");
        joinedLobby = true;
    }

    public override void OnPhotonJoinRoomFailed(object[] codeAndMsg)
    {
        Debug.LogWarning("No Random Room Found");
    }

    public override void OnJoinedRoom()
    {
        /*if (isConnecting)
        {*/
        Debug.LogWarning("Joined Room");
        PhotonNetwork.LoadLevel("IntegrateScene");
        //}
    }

    public override void OnCreatedRoom()
    {
        Debug.LogWarning("Room Created");
    }
    #endregion
}
