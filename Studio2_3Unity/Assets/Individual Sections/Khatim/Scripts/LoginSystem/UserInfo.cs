using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInfo : MonoBehaviour
{
    #region Public Variables
    public static UserInfo instance = null;
    public string username;
    #endregion

    #region Private Variables

    #endregion

    #region Unity Callbacks
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }
    #endregion

    #region My Functions
    public void UserSet(string user)
    {
        PhotonNetwork.playerName = user;
        PhotonNetwork.player.NickName = user;
        Debug.LogWarning("Username Set");
    }
    #endregion

}
