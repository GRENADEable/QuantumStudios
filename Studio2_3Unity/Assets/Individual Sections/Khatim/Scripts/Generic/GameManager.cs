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
        if (PlayerManager.localPlyInstance == null)
        {
            Debug.Log("Spawning Player From: " + SceneManager.GetActiveScene().name);
            PhotonNetwork.Instantiate(this.plyPrefab.name, new Vector3(0f, 15f, 0f), Quaternion.identity, 0);
        }
        else
        {
            Debug.Log("Ignoring scene load for " + SceneManager.GetActiveScene().name);
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
        PhotonNetwork.LoadLevel("IntegrateScene");
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
