using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InsertData : MonoBehaviour
{
    #region Public Variables
    public InputField createUser;
    public InputField createPassword;
    public InputField createEmail;
    #endregion

    #region Private Variables
    private string userURL = "https://kahtimdar.000webhostapp.com/adduser.php";
    //private string userURL = "http://localhost/unity_login_system/adduser.php";
    #endregion

    #region Callbacks.
    void Update()
    {
        //For Testing
        /*if (Input.GetKeyDown(KeyCode.C))
            CreateUser(inputUser, inputPassword, inputEmail);*/
    }
    #endregion

    #region My Functions
    public void Create()
    {
        CreateUser(createUser.text, createPassword.text, createEmail.text);
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
}
