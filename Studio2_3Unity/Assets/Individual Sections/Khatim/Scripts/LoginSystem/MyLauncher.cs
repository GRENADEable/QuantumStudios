using System;
using System.Collections;
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
    public GameObject tCPanel;
    public GameObject leaderboardPanel;
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
    [Header("Leaderboard")]
    public string[] leaderboardArray;
    #endregion

    #region Private Variables
    private string LoginURL = "https://kahtimdar.000webhostapp.com/usernamelogin.php"; //External Database
    //private string LoginURL = "http://localhost/unity_login_system/usernamelogin.php"; //Local Databas
    private string userURL = "https://kahtimdar.000webhostapp.com/adduser.php"; //External Database
    //private string userURL = "http://localhost/unity_login_system/adduser.php"; //Local Database
    //private string leaderboardURL = "http://localhost/highscore_unity/displayScore.php"; //Local Databse
    private string leaderboardURL = "https://kahtimdar.000webhostapp.com/displayScore.php"; //External Databse
    [SerializeField]
    private Text[] leaderboardTextArray;
    private bool seen;
    #endregion

    #region Callbacks

    void Awake()
    {
        if (PhotonNetwork.connected)
            PhotonNetwork.Disconnect();

        if (!seen)
            tCPanel.SetActive(true);
        else
            tCPanel.SetActive(false);


        multiplayerPanel.SetActive(false);
        lobbyPanel.SetActive(false);

        EnterUsernamePanel.SetActive(false);
        loginUserPanel.SetActive(false);
        loginAndCreateUserPanel.SetActive(false);

        gameLoadingText.SetActive(false);

        mainMenuPanel.SetActive(false);
        mainMenuButton.SetActive(false);

        leaderboardPanel.SetActive(false);
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

    void FixedUpdate()
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
    #region Cleanup

    /*public void SingleplayerPanel()
    {
        EnterUsernamePanel.SetActive(true);
        mainMenuPanel.SetActive(false);
    }*/
    #endregion

    public void LoadingPanel()
    {
        #region Cleanup
        //EnterUsernamePanel.SetActive(false);
        //gameLoadingText.SetActive(true);
        //UserInfo.instance.username = highscoreUser.text;
        #endregion
        if (highscoreUser.text != "")
        {
            high.playerName = highscoreUser.text;
            Debug.LogWarning("Username Added to Scriptable Objects");
            EnterUsernamePanel.SetActive(false);
            gameLoadingText.SetActive(true);
            SceneManagement.instance.SinglePlayer();
        }
    }
    public void MainMenuPanel()
    {
        timer = 6.0f;
        seen = true;
        loginText.SetActive(false);
        #region Cleanup
        /*lobbyPanel.SetActive(false);
        multiplayerPanel.SetActive(false);
        mainMenuButton.SetActive(false);
        gameLoadingText.SetActive(false);
        mainMenuPanel.SetActive(true);*/
        if (PhotonNetwork.connected)
            PhotonNetwork.Disconnect();
        #endregion
    }

    public void MultiplayerPanel()
    {
        creatingUserText.SetActive(false);
        createdUserText.SetActive(false);
        #region Cleanup
        /*loginText.SetActive(false);
        mainMenuPanel.SetActive(false);
        multiplayerPanel.SetActive(true);
        createUserPanel.SetActive(false);
        loginUserPanel.SetActive(false);
        loginAndCreateUserPanel.SetActive(true);*/
        #endregion
    }

    #region Cleanup
    public void CreateUserPanel()
    {
        /*loginText.SetActive(false);
        loginAndCreateUserPanel.SetActive(false);*/
        //createUserPanel.SetActive(true);
        //creatingUserText.SetActive(false);
        //createdUserText.SetActive(false);
    }
    public void LoginPanel()
    {
        //creatingUserText.SetActive(false);
        /*createdUserText.SetActive(false);
        loginAndCreateUserPanel.SetActive(false);*/
        /*userNotFoundText.SetActive(false);
        passwordWrongText.SetActive(false);*/
        //loginUserPanel.SetActive(true);
    }
    #endregion

    public void JoinLobby()
    {
        loginText.SetActive(false);
        #region Cleanup
        /*loginAndCreateUserPanel.SetActive(false);
        loginUserPanel.SetActive(false);
        createUserPanel.SetActive(false);*/
        #endregion
        lobbyPanel.SetActive(true);
    }

    public void Login()
    {
        #region Cleanup
        //userNotFoundText.SetActive(false);
        //passwordWrongText.SetActive(false);
        #endregion
        if (inputUser.text != "" || inputPassword.text != "")
        {
            loginUserPanel.SetActive(false);
            loginText.SetActive(true);
            StartCoroutine(DBLogin(inputUser.text, inputPassword.text));
            Debug.LogWarning("Started DBLogin");

            plyName.uesrName = inputUser.text;
            Debug.LogWarning("Username Stored");
        }

        #region Cleanup
        //PhotonNetwork.player.NickName = inputUser.text;
        //PhotonNetwork.player.NickName = inputUser.text;
        #endregion
    }

    public void Leaderboard()
    {
        StartCoroutine(LoadLeaderboard());
    }

    public void Create()
    {
        if (createUser.text != "" || createEmail.text != "" || createPassword.text != "")
        {
            creatingUserText.SetActive(true);
            CreateUser(createUser.text, createPassword.text, createEmail.text);
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
            passwordWrongText.SetActive(false);
            loginUserPanel.SetActive(true);
            userNotFoundText.SetActive(true);
            yield return null;
        }
        else if (dbLink.text == "Password is Wrong")
        {
            loginText.SetActive(false);
            userNotFoundText.SetActive(false);
            loginUserPanel.SetActive(true);
            passwordWrongText.SetActive(true);
            yield return null;
        }
    }

    private IEnumerator LoadLeaderboard()
    {
        WWW leaderboard = new WWW(leaderboardURL);
        yield return leaderboard;

        string leaderboardString = leaderboard.text;
        leaderboardArray = leaderboardString.Split(';');
        //Array.Sort(leaderboardArray);

        for (int i = 0; i < leaderboardArray.Length; i++)
        {
            leaderboardTextArray[i].text = leaderboardArray[i];
        }
    }
    #endregion

}
