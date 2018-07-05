using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsertData : MonoBehaviour
{
    #region Public Variables
    public string inputUser;
    public string inputPassword;
    public string inputEmail;
    #endregion

    #region Private Variables
    private string userURL = "http://localhost/unity_login_system/adduser.php";
    #endregion

    #region Callbacks.
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            CreateUser(inputUser, inputPassword, inputEmail);
    }
    #endregion

    #region My Functions
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
}
