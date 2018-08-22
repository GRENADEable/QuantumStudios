using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : Photon.PunBehaviour
{
    #region  Public Variables
    public GameObject ply1;
    //public GameObject ply2;
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
        if (ply1 != null && PhotonNetwork.isMasterClient)
        {
            Debug.Log("Spawning Player From: " + SceneManager.GetActiveScene().name);
            PhotonNetwork.Instantiate(this.ply1.name, new Vector3(115.0f, 1.0f, 75.0f), Quaternion.identity, 0);
            this.ply1.SetActive(true);
        }

        /*if (PhotonNetwork.room.PlayerCount > 1)
        {
            PhotonNetwork.Instantiate(this.ply2.name, new Vector3(122.0f, 1.0f, 55.0f), Quaternion.identity, 0);
            this.ply2.SetActive(true);
        }*/
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
