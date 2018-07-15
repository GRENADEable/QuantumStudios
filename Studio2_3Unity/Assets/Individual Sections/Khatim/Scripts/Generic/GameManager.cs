using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Photon.PunBehaviour
{
    #region  Public Variables
    public GameObject plyPrefab;
    public static GameObject localPlyInstance;
    #endregion

    #region  Private Variables

    #endregion

    #region Unity Callbacks
    void Awake()
    {
        
    }
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

    #region My Functions

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    void LoadMap()
    {
        if (!PhotonNetwork.isMasterClient)
        {
            Debug.LogError("No Master Client Found");
        }
        Debug.LogWarning("Loading Level: " + SceneManager.GetActiveScene());
        PhotonNetwork.LoadLevel("KhatimScene");
    }
    #endregion
    #region Photon Callbacks
    public override void OnLeftRoom()
    {
        SceneManager.LoadScene("LoginTestScene");
    }

    public override void OnPhotonPlayerConnected(PhotonPlayer newPlayer)
    {
        if (PhotonNetwork.isMasterClient)
        {
            Debug.Log("OnPhotonPlayerConnected isMasterClient " + PhotonNetwork.isMasterClient);
            LoadMap();
        }
    }
    #endregion
}
