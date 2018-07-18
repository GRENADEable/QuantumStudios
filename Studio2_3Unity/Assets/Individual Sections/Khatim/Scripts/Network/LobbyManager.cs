using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LobbyManager : Photon.MonoBehaviour
{
    #region  Public Variables
    public InputField roomName;
    public GameObject roomButtonPrefab;
    public PhotonLogLevel logLevel = PhotonLogLevel.Informational;
    #endregion

    #region  Private Variables
    private static LobbyManager instance;
    private string version = "1";
    private List<GameObject> roomButtonPrefabs = new List<GameObject>();
    private bool joinedLobby;
    private byte maxPlayers = 4;
    #endregion

    #region Unity Callbacks
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject.transform);
        }
        else if (instance != this)
            Destroy(gameObject);

        PhotonNetwork.autoJoinLobby = false;
        PhotonNetwork.automaticallySyncScene = true;
    }

    void Start()
    {
        PhotonNetwork.ConnectUsingSettings(version);
        joinedLobby = false;
    }
    #endregion

    #region My Functions
    public void CreateRoom()
    {
        if (PhotonNetwork.JoinLobby())
        {
            Debug.LogWarning("Creating Room");
            RoomOptions roomOpt = new RoomOptions()
            {
                IsVisible = true,
                IsOpen = true,
                MaxPlayers = maxPlayers
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
    void OnConnectedToMaster()
    {
        Debug.LogWarning("Connectng to Master");
        PhotonNetwork.JoinLobby();
    }

    void OnGUI()
    {
        GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
    }
    void OnJoinedLobby()
    {
        Debug.LogWarning("Joined Lobby");
        joinedLobby = true;
    }

    void OnPhotonJoinRoomFailed(object[] codeAndMsg)
    {
        Debug.LogWarning("No Random Room Found");
    }

    void OnJoinedRoom()
    {
        Debug.LogWarning("Joined Room");
        SceneManager.LoadScene("IntegrateScene");
    }

    void OnCreatedRoom()
    {
        Debug.LogWarning("Room Created");
    }
    #endregion
}
