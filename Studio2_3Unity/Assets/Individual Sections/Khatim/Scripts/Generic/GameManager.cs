using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : Photon.PunBehaviour
{
    #region  Public Variables
    [Header("Players")]
    public GameObject player1;
    public GameObject player2;
    public GameObject player3;
    public GameObject player4;
    //[Header("Tiemrs")]
    //public float respawnTimer;
    //public float maxRespawnTime;
    [Header("Camera")]
    public GameObject cam;
    public Vector3 camOffset;
    public static GameManager instance;
    [Header("Spwan Location")]
    public GameObject[] playerSpawnLocation;
    [Header("Index")]
    public int index;
    #endregion

    #region Unity Callbacks
    void Awake()
    {
        if (instance == null)
            instance = this;

        else if (instance != null)
            Destroy(gameObject);

        if (PhotonNetwork.connected)
        {
            playerSpawnLocation = GameObject.FindGameObjectsWithTag("SpawnPlayer");
            index = Random.Range(0, playerSpawnLocation.Length);

            if (player1 != null && PhotonNetwork.room.PlayerCount == 1)
            {
                PhotonNetwork.Instantiate(player1.name, playerSpawnLocation[index].transform.position, Quaternion.identity, 0);
                player1 = GameObject.FindGameObjectWithTag("Player1");
                Instantiate(cam, player1.transform.position + camOffset, Quaternion.Euler(45.0f, 0f, 0f));
            }

            if (player2 != null && PhotonNetwork.room.PlayerCount == 2)
            {
                PhotonNetwork.Instantiate(player2.name, playerSpawnLocation[index].transform.position, Quaternion.identity, 0);
                player2 = GameObject.FindGameObjectWithTag("Player2");
                Instantiate(cam, player2.transform.position + camOffset, Quaternion.Euler(45.0f, 0f, 0f));
            }

            if (player3 != null && PhotonNetwork.room.PlayerCount == 3)
            {
                PhotonNetwork.Instantiate(player3.name, playerSpawnLocation[index].transform.position, Quaternion.identity, 0);
                player3 = GameObject.FindGameObjectWithTag("Player3");
                Instantiate(cam, player3.transform.position + camOffset, Quaternion.Euler(45.0f, 0f, 0f));
            }

            if (player4 != null && PhotonNetwork.room.PlayerCount == 4)
            {
                PhotonNetwork.Instantiate(player4.name, playerSpawnLocation[index].transform.position, Quaternion.identity, 0);
                player4 = GameObject.FindGameObjectWithTag("Player4");
                Instantiate(cam, player4.transform.position + camOffset, Quaternion.Euler(45.0f, 0f, 0f));
            }

            //respawnTimer = maxRespawnTime;
        }
    }

    /*void Update()
    {
        if (!player1.activeInHierarchy)
        {
            respawnTimer -= Time.deltaTime;
            if (respawnTimer <= 0)
            {
                player1.SetActive(true);
                respawnTimer = maxRespawnTime;
            }
        }
    }*/
    #endregion

    #region My Functions
    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        AudioManager.instance.StopAudio();
        AudioManager.instance.AudioAccess(9);
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
        SceneManager.LoadScene("MainMenu");
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
