using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MyLauncher : Photon.MonoBehaviour
{
    #region Public Variables
    [Header("Panels")]
    public GameObject createUserPanel;
    public GameObject loginUserPanel;
    public GameObject mainMenuPanel;
    public GameObject lobbyPanel;
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

    void Awake()
    {
        PhotonNetwork.autoJoinLobby = false;
    }
    void Start()
    {
        createdUserText.SetActive(false);
        creatingUserText.SetActive(false);
        passwordWrongText.SetActive(false);
        userNotFoundText.SetActive(false);
        lobbyPanel.SetActive(false);
        loginText.SetActive(false);
        createUserPanel.SetActive(false);
        loginUserPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }
    #endregion

    #region My Functions
    public void MainMenuPanel()
    {
        createUserPanel.SetActive(false);
        loginUserPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
        createdUserText.SetActive(false);
        creatingUserText.SetActive(false);
        passwordWrongText.SetActive(false);
        userNotFoundText.SetActive(false);
    }
    public void CreateUserPanel()
    {
        createUserPanel.SetActive(true);
        mainMenuPanel.SetActive(false);
    }
    public void LoginPanel()
    {
        mainMenuPanel.SetActive(false);
        loginUserPanel.SetActive(true);
    }
    public void JoinLobby()
    {
        loginText.SetActive(false);
        mainMenuPanel.SetActive(false);
        loginUserPanel.SetActive(false);
        createUserPanel.SetActive(false);
        lobbyPanel.SetActive(true);
    }
    public void Login()
    {
        userNotFoundText.SetActive(false);
        passwordWrongText.SetActive(false);
        StartCoroutine(DBLogin(inputUser.text, inputPassword.text));
    }
    public void Create()
    {
        creatingUserText.SetActive(true);
        CreateUser(createUser.text, createPassword.text, createEmail.text);
    }

    #endregion

    #region  IEnums
    public IEnumerator DBLogin(string playerName, string password)
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
    }
    #endregion
}
