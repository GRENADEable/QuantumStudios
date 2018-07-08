using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginScript : MonoBehaviour
{

    #region Public Variables
    public string inputUser;
    public string inputPassword;
    #endregion

    #region Private Variables
    private string userURL = "https://kahtimdar.000webhostapp.com/usernamelogin.php";
    //private string userURL = "http://localhost/unity_login_system/usernamelogin.php";
    #endregion

    #region Callbacks
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
            StartCoroutine(DBLogin(inputUser, inputPassword));
    }
    #endregion

    #region My Functions
    IEnumerator DBLogin(string playerName, string password)
    {
        WWWForm loginform = new WWWForm();
        loginform.AddField("playerUsername", playerName);
        loginform.AddField("playerPassword", password);
        WWW dbLink = new WWW(userURL, loginform);

        yield return dbLink;
        Debug.Log(dbLink.text);
    }
    #endregion
}
