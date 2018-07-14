using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoginScript : MonoBehaviour
{

    #region Public Variables
    public InputField inputUser;
    public InputField inputPassword;
    #endregion

    #region Private Variables

    #endregion

    #region Private Variables
    private string userURL = "https://kahtimdar.000webhostapp.com/usernamelogin.php";
    //private string userURL = "http://localhost/unity_login_system/usernamelogin.php";
    #endregion

    #region Callbacks
    void Update()
    {
        //For Testing
        /*if (Input.GetKeyDown(KeyCode.L))
            StartCoroutine(DBLogin(inputUser, inputPassword));*/
    }
    #endregion

    #region My Functions
    public void Login()
    {
        StartCoroutine(DBLogin(inputUser.text, inputPassword.text));
    }
    public IEnumerator DBLogin(string playerName, string password)
    {
        WWWForm loginform = new WWWForm();
        loginform.AddField("playerUsername", playerName);
        loginform.AddField("playerPassword", password);
        WWW dbLink = new WWW(userURL, loginform);

        yield return dbLink;
        SceneManager.LoadScene("ShariqScene");
        Debug.Log(dbLink.text);
    }
    #endregion
}
