using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadData : MonoBehaviour
{
    #region Public Variables
    public string[] users;
    #endregion

    #region Private Variables

    #endregion

    #region Callbacks
    private IEnumerator Start()
    {
        WWW userData = new WWW("http://localhost/unity_login_system/UsernameLogin.php");
        yield return userData;
        string userDataString = userData.text;
        print(userDataString);
        users = userDataString.Split(';');
        print(GetUserValue(users[0], "Password:"));
    }
    #endregion

    #region My Functions
    private string GetUserValue(string data, string index)
    {
        string value = data.Substring(data.IndexOf(index) + index.Length);
        if (value.Contains("|"))
            value = value.Remove(value.IndexOf("|"));

        return value;
    }
    #endregion
}
