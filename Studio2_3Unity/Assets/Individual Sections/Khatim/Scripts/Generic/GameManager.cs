using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : Photon.PunBehaviour
{
    #region  Public Variables
    public GameObject plyPrefab;
    public static GameManager instance = null;
    #endregion

    #region  Private Variables
    #endregion

    #region Unity Callbacks
    void Awake()
    {
        if (instance == null)
            instance = this;

        else if (instance != null)
            Destroy(gameObject);
    }
    void Start()
    {
        if (plyPrefab != null)
        {
            Debug.Log("Spawning Player From: " + SceneManager.GetActiveScene().name);
            PhotonNetwork.Instantiate(this.plyPrefab.name, new Vector3(115.0f, 1.0f, 75.0f), Quaternion.identity, 0);
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
            Debug.LogWarning("Load Level Faild. We are not Master");
        }
        Debug.Log("Level being Loaded");
    }
    #endregion
    #region Photon Callbacks
    public override void OnLeftRoom()
    {
        SceneManager.LoadScene("LobbyScene");
    }
    public override void OnPhotonPlayerConnected(PhotonPlayer newPlayer)
    {
        Debug.LogWarning("OnPhotonPlayerConnected" + newPlayer.NickName);

        if (PhotonNetwork.isMasterClient)
        {
            Debug.LogWarning("OnPhotonPlayerConnected isMasterClient" + PhotonNetwork.isMasterClient);
            LoadMap();
        }
    }
    #endregion
}
