using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Photon.PunBehaviour
{
    #region  Public Variables
    public GameObject plyPrefab;
    #endregion

    #region  Private Variables

    #endregion

    #region Unity Callbacks
    void Start()
    {
        if (plyPrefab == null)
        {
            Debug.LogError("<Color=Red><a>Missing</a></Color> playerPrefab Reference. Please set it up in GameObject 'Game Manager'", this);
        }
        else
        {
            Debug.Log("We are Instantiating LocalPlayer from " + SceneManager.GetActiveScene().name);
            PhotonNetwork.Instantiate(this.plyPrefab.name, new Vector3(0f, 5f, 0f), Quaternion.identity, 0);
        }
    }
    #endregion

    #region Photon Callbacks

    #endregion
}
