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
    #endregion

    #region  Private Variables
    private static LobbyManager instance;
    private string version = "1";
    private List<GameObject> roomButtonPrefabs = new List<GameObject>();
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
                MaxPlayers = 4
            };

            PhotonNetwork.CreateRoom(roomName.text, roomOpt, TypedLobby.Default);
            Debug.LogWarning("Created Room with Lobby Name " + roomName.text);
        }
    }

    public void RefreshRoom()
    {
        //if (PhotonNetwork.JoinLobby())
        //{
        if (roomButtonPrefabs.Count > 0)
        {
            for (int i = 0; i < roomButtonPrefabs.Count; i++)
            {
                Destroy(roomButtonPrefabs[i]);
            }
        }
        roomButtonPrefabs.Clear();
        //}

        for (int i = 0; i < PhotonNetwork.GetRoomList().Length; i++)
        {
            Debug.LogWarning(PhotonNetwork.GetRoomList()[i].Name);
            GameObject room = Instantiate(roomButtonPrefab);
            room.transform.SetParent(roomButtonPrefab.transform.parent);

            room.GetComponent<RectTransform>().localScale = roomButtonPrefab.GetComponent<RectTransform>().localScale;
            room.GetComponent<RectTransform>().localPosition = new Vector3(roomButtonPrefab.GetComponent<RectTransform>().localPosition.x, roomButtonPrefab.GetComponent<RectTransform>().localPosition.y - (i * 50), roomButtonPrefab.GetComponent<RectTransform>().localPosition.z);
            room.gameObject.transform.Find("RoomNameText").GetComponent<Text>().text = PhotonNetwork.GetRoomList()[i].Name;
            room.gameObject.transform.Find("PlayerCount").GetComponent<Text>().text = PhotonNetwork.GetRoomList()[i].PlayerCount + "/" + PhotonNetwork.GetRoomList()[i].MaxPlayers;

            room.SetActive(true);
            roomButtonPrefabs.Add(room);

        }
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
        Invoke("RefreshRoom", 0.2f);
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
