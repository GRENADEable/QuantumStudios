using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MyLauncher : MonoBehaviour
{
    #region Public Variables
    [Header("Panels")]
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
    [Header("Login Field")]
    public InputField inputUser;
    public InputField inputPassword;
    [Header("Create Field")]
    public InputField createUser;
    public InputField createPassword;
    public InputField createEmail;
    #endregion

    #region Private Variables
    private string LoginURL = "https://kahtimdar.000webhostapp.com/usernamelogin.php";
    //private string userURL = "http://localhost/unity_login_system/usernamelogin.php";

    private string userURL = "https://kahtimdar.000webhostapp.com/adduser.php";
    //private string userURL = "http://localhost/unity_login_system/adduser.php";
    #endregion

    #region Callbacks

    private void Awake()
    {
        if (PhotonNetwork.connected)
            PhotonNetwork.Disconnect();

        lobbyPanel.SetActive(false);
        loginUserPanel.SetActive(false);
        loginAndCreateUserPanel.SetActive(false);
        multiplayerPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }
    #endregion

    #region My Functions
    public void MainMenuPanel()
    {
        lobbyPanel.SetActive(false);
        multiplayerPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }

    public void MultiplayerPanel()
    {
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
        PhotonNetwork.player.NickName = inputUser.text;
    }

    public void Create()
    {
        creatingUserText.SetActive(true);
        CreateUser(createUser.text, createPassword.text, createEmail.text);
    }

    public void Singleplayer()
    {
        SceneManager.LoadScene("AjayScene");
    }
    public void Quit()
    {
        Application.Quit();
        Debug.LogWarning("Application Closed");
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
        creatingUserText.SetActive(false);
        createdUserText.SetActive(true);
        Debug.Log("Databse Accessed"); //Testing

        creatingUserText.SetActive(false);
        createdUserText.SetActive(true);
    }
    #endregion
}
