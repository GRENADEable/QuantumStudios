using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MyLauncher : Photon.PunBehaviour
{
    #region Public Variables
    public PhotonLogLevel logLevel = PhotonLogLevel.Informational;

    [Tooltip("Maximum Number of Players")]
    public byte maxPlayersInRoom = 4;

    [Tooltip("The Ui Panel to let the user enter name, connect and play")]
    public GameObject usernameLoginPanel;
    [Tooltip("The UI Label to inform the user that the connection is in progress")]
    [Header("Lobby")]
    public string roomName;
    [Header("Login Field")]
    public InputField inputUser;
    public InputField inputPassword;
    [Header("Create Field")]
    public InputField createUser;
    public InputField createPassword;
    public InputField createEmail;
    #endregion

    #region Private Variables
    private string version = "1";
    private bool isConnecting;
    private string LoginURL = "https://kahtimdar.000webhostapp.com/usernamelogin.php";
    //private string userURL = "http://localhost/unity_login_system/usernamelogin.php";

    private string userURL = "https://kahtimdar.000webhostapp.com/adduser.php";
    //private string userURL = "http://localhost/unity_login_system/adduser.php";
    #endregion

    #region Callbacks

    void Awake()
    {
        PhotonNetwork.autoJoinLobby = false;
        PhotonNetwork.automaticallySyncScene = true;
    }
    void Start()
    {
        usernameLoginPanel.SetActive(true);
    }
    #endregion

    #region My Functions
    public void ConnectToServer()
    {
        isConnecting = true;

        if (PhotonNetwork.connected)
        {
            usernameLoginPanel.SetActive(false);
            //PhotonNetwork.JoinRandomRoom();
        }
        else
            PhotonNetwork.ConnectUsingSettings(version);
    }

    public void JoinRoom(RoomInfo room)
    {
        PhotonNetwork.JoinRoom(room.Name);
    }
    public void Login()
    {
        StartCoroutine(DBLogin(inputUser.text, inputPassword.text));
    }
    public void Create()
    {
        CreateUser(createUser.text, createPassword.text, createEmail.text);
    }

    #endregion

    #region  IEnums
    public IEnumerator DBLogin(string playerName, string password)
    {
        WWWForm loginform = new WWWForm();
        loginform.AddField("playerUsername", playerName);
        loginform.AddField("playerPassword", password);
        WWW dbLink = new WWW(LoginURL, loginform);
        yield return dbLink;

        Debug.Log(dbLink.text);

        if (dbLink.text == "Login Success")
        {
            ConnectToServer();
        }
        else
            yield return null;
    }
    public void CreateUser(string playerName, string password, string email)
    {
        WWWForm loginform = new WWWForm();
        Debug.Log("WWWForm Created");

        loginform.AddField("playerUsername", playerName);
        Debug.Log("Username Field Added");

        loginform.AddField("playerPassword", password);
        Debug.Log("Password Field Added");

        loginform.AddField("playerEmail", email);
        Debug.Log("Email Field Added");

        WWW dbLink = new WWW(userURL, loginform);
        Debug.Log("Databse Accessed");
    }
    #endregion
    #region PunCallbacks
    public override void OnConnectedToMaster()
    {
        if (isConnecting)
        {
            Debug.LogWarning("Connected To Master");
            PhotonNetwork.JoinRandomRoom();
        }
    }

    public override void OnDisconnectedFromPhoton()
    {
        usernameLoginPanel.SetActive(true);
        isConnecting = false;
        Debug.LogWarning("Disconnected from Photon");
    }

    public override void OnPhotonRandomJoinFailed(object[] codeAndMsg)
    {
        Debug.LogWarning("No Random Room Found");

        PhotonNetwork.CreateRoom(null, new RoomOptions()
        {
            MaxPlayers = maxPlayersInRoom
        },
        null);
    }

    public override void OnJoinedRoom()
    {
        if (PhotonNetwork.room.PlayerCount == 1)
        {
            Debug.LogWarning("Loading Scene: " + SceneManager.GetActiveScene().name);
            PhotonNetwork.LoadLevel("IntegrateScene");
        }
    }
    #endregion
}
