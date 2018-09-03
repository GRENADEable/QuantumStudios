using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MyLauncher : MonoBehaviour
{
    #region Public Variables
    [Header("Panels")]
    public GameObject EnterUsernamePanel;
    public GameObject createUserPanel;
    public GameObject loginUserPanel;
    public GameObject loginAndCreateUserPanel;
    public GameObject lobbyPanel;
    public GameObject mainMenuPanel;
    public GameObject multiplayerPanel;
    [Header("Notification Text")]
    public GameObject userNotFoundText;
    public GameObject passwordWrongText;
    public GameObject loginText;
    public GameObject creatingUserText;
    public GameObject createdUserText;
    public GameObject gameLoadingText;
    public GameObject mainMenuButton;
    [Header("Login Field")]
    public InputField inputUser;
    public InputField inputPassword;
    public float timer;
    [Header("Create Field")]
    public InputField createUser;
    public InputField createPassword;
    public InputField createEmail;
    public PlayerNameObj plyName;
    public HighScoreObj high;
    public InputField highscoreUser;
    #endregion

    #region Private Variables
    private string LoginURL = "https://kahtimdar.000webhostapp.com/usernamelogin.php"; //External Database
    //private string LoginURL = "http://localhost/unity_login_system/usernamelogin.php"; //Local Database

    private string userURL = "https://kahtimdar.000webhostapp.com/adduser.php"; //External Database
    //private string userURL = "http://localhost/unity_login_system/adduser.php"; //Local Database
    #endregion

    #region Callbacks

    void Awake()
    {
        if (PhotonNetwork.connected)
            PhotonNetwork.Disconnect();

        lobbyPanel.SetActive(false);
        EnterUsernamePanel.SetActive(false);
        loginUserPanel.SetActive(false);
        loginAndCreateUserPanel.SetActive(false);
        multiplayerPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
        mainMenuButton.SetActive(false);
    }

    void Start()
    {
        createUser.characterLimit = 20;
        createPassword.characterLimit = 16;

        inputUser.characterLimit = 20;
        inputPassword.characterLimit = 16;

        highscoreUser.characterLimit = 20;

        timer = 6.0f;
    }

    void Update()
    {
        if (loginText.activeInHierarchy)
        {
            timer -= Time.deltaTime;
        }

        if (timer <= 0f)
        {
            mainMenuButton.SetActive(true);
        }
    }
    #endregion

    #region My Functions
    public void SingleplayerPanel()
    {
        EnterUsernamePanel.SetActive(true);
        mainMenuPanel.SetActive(false);
    }

    public void LoadingPanel()
    {
        EnterUsernamePanel.SetActive(false);
        gameLoadingText.SetActive(true);
        //UserInfo.instance.username = highscoreUser.text;
        high.playerName = highscoreUser.text;
        Debug.LogWarning("Username Added to Scriptable Objects");
    }
    public void MainMenuPanel()
    {
        timer = 6.0f;
        lobbyPanel.SetActive(false);
        multiplayerPanel.SetActive(false);
        mainMenuButton.SetActive(false);
        gameLoadingText.SetActive(false);
        mainMenuPanel.SetActive(true);
    }

    public void MultiplayerPanel()
    {
        creatingUserText.SetActive(false);
        createdUserText.SetActive(false);
        loginText.SetActive(false);
        mainMenuPanel.SetActive(false);
        multiplayerPanel.SetActive(true);
        createUserPanel.SetActive(false);
        loginUserPanel.SetActive(false);
        loginAndCreateUserPanel.SetActive(true);
    }

    public void CreateUserPanel()
    {
        loginText.SetActive(false);
        loginAndCreateUserPanel.SetActive(false);
        creatingUserText.SetActive(false);
        createdUserText.SetActive(false);
        createUserPanel.SetActive(true);
    }
    public void LoginPanel()
    {
        creatingUserText.SetActive(false);
        createdUserText.SetActive(false);
        loginAndCreateUserPanel.SetActive(false);
        userNotFoundText.SetActive(false);
        passwordWrongText.SetActive(false);
        loginUserPanel.SetActive(true);
    }

    public void JoinLobby()
    {
        loginText.SetActive(false);
        loginAndCreateUserPanel.SetActive(false);
        loginUserPanel.SetActive(false);
        createUserPanel.SetActive(false);
        lobbyPanel.SetActive(true);
    }

    public void Login()
    {
        userNotFoundText.SetActive(false);
        passwordWrongText.SetActive(false);
        StartCoroutine(DBLogin(inputUser.text, inputPassword.text));
        //PhotonNetwork.player.NickName = inputUser.text;
        plyName.uesrName = inputUser.text;
        Debug.LogWarning("Username Stored");
        //PhotonNetwork.player.NickName = inputUser.text;
    }

    public void Create()
    {
        creatingUserText.SetActive(true);
        CreateUser(createUser.text, createPassword.text, createEmail.text);
    }
    #endregion

    #region IEnums
    private IEnumerator DBLogin(string playerName, string password)
    {
        loginText.SetActive(true);
        loginUserPanel.SetActive(false);
        WWWForm loginform = new WWWForm();
        loginform.AddField("playerUsername", playerName);
        loginform.AddField("playerPassword", password);
        WWW dbLink = new WWW(LoginURL, loginform);
        yield return dbLink;

        Debug.Log(dbLink.text);

        if (dbLink.text == "Login Success")
        {
            JoinLobby();
        }
        else if (dbLink.text == "User Not Found")
        {
            loginText.SetActive(false);
            loginUserPanel.SetActive(true);
            userNotFoundText.SetActive(true);
            yield return null;
        }
        else if (dbLink.text == "Password is Wrong")
        {
            loginText.SetActive(false);
            loginUserPanel.SetActive(true);
            passwordWrongText.SetActive(true);
            yield return null;
        }
    }
    public void CreateUser(string playerName, string password, string email)
    {
        WWWForm loginform = new WWWForm();
        Debug.Log("WWWForm Created"); //Testing

        loginform.AddField("playerUsername", playerName);
        Debug.Log("Username Field Added"); //Testing

        loginform.AddField("playerPassword", password);
        Debug.Log("Password Field Added"); //Testing

        loginform.AddField("playerEmail", email);
        Debug.Log("Email Field Added"); //Testing

        WWW dbLink = new WWW(userURL, loginform);
        loginAndCreateUserPanel.SetActive(true);
        createUserPanel.SetActive(false);
        creatingUserText.SetActive(false);
        createdUserText.SetActive(true);
        Debug.Log("Databse Accessed"); //Testing

        /*creatingUserText.SetActive(false);
        createdUserText.SetActive(true);*/
    }
    #endregion
}
