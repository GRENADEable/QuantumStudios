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
    //[SerializeField]
    //private CameraFollow cam;
    #endregion

    #region Unity Callbacks
    void Awake()
    {

    }
    void Start()
    {
        Debug.Log("Spawning Player From: " + SceneManager.GetActiveScene().name);
        PhotonNetwork.Instantiate(this.plyPrefab.name, new Vector3(0f, 5f, 0f), Quaternion.identity, 0);

        //cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFollow>();
    }
    #endregion

    #region My Functions

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        //PhotonNetwork.Disconnect();
        //PhotonNetwork.LeaveRoom();
    }

    void LoadMap()
    {
        if (!PhotonNetwork.isMasterClient)
        {
            Debug.LogWarning("Load Level Faild. We are not Master");
        }
        Debug.Log("Level being Loaded");
        //PhotonNetwork.LoadLevel("IntegrateScene");
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
