using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : Photon.PunBehaviour
{
    #region  Public Variables
    public GameObject player1;
    public GameObject player2;
    public GameObject cam;
    public Vector3 camOffset;
    public static GameManager instance;
    public GameObject[] spawnLocation;
    public int index;
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

        if (PhotonNetwork.connected)
        {
            spawnLocation = GameObject.FindGameObjectsWithTag("SpawnPlayer");
            index = Random.Range(0, spawnLocation.Length);

            if (player1 != null && PhotonNetwork.room.PlayerCount == 1)
            {
                PhotonNetwork.Instantiate(player1.name, spawnLocation[index].transform.position, Quaternion.identity, 0);
                player1 = GameObject.FindGameObjectWithTag("Player1");
                Instantiate(cam, player1.transform.position + camOffset, Quaternion.Euler(45.0f, 0f, 0f));
            }

            if (player2 != null && PhotonNetwork.room.PlayerCount == 2)
            {
                PhotonNetwork.Instantiate(player2.name, spawnLocation[index].transform.position, Quaternion.identity, 0);
                player2 = GameObject.FindGameObjectWithTag("Player2");
                Instantiate(cam, player2.transform.position + camOffset, Quaternion.Euler(45.0f, 0f, 0f));
            }
        }
    }
    /*void Start()
    {
        if (PhotonNetwork.connected)
        {
            spawnLocation = GameObject.FindGameObjectsWithTag("SpawnPlayer");
            index = Random.Range(0, spawnLocation.Length);

            if (player1 != null && PhotonNetwork.room.PlayerCount == 1)
            {
                PhotonNetwork.Instantiate(player1.name, spawnLocation[index].transform.position, Quaternion.identity, 0);
                player1 = GameObject.FindGameObjectWithTag("Player1");
                Instantiate(cam, player1.transform.position + camOffset, Quaternion.Euler(45.0f, 0f, 0f));
            }

            if (player2 != null && PhotonNetwork.room.PlayerCount == 2)
            {
                PhotonNetwork.Instantiate(player2.name, spawnLocation[index].transform.position, Quaternion.identity, 0);
                player2 = GameObject.FindGameObjectWithTag("Player2");
                Instantiate(cam, player2.transform.position + camOffset, Quaternion.Euler(45.0f, 0f, 0f));
            }
        }
    }*/

    /*void Update()
    {
        if (PhotonNetwork.connected)
        {
            if (PhotonNetwork.room.PlayerCount == 1 && noOfPlayers == 0)
            {
                PhotonNetwork.Instantiate(players[0].name, spawnLocation[0].transform.position, Quaternion.identity, 0);
                noOfPlayers = 1;
            }
            else if (PhotonNetwork.room.PlayerCount == 2 && noOfPlayers == 1)
            {
                PhotonNetwork.Instantiate(players[1].name, spawnLocation[1].transform.position, Quaternion.identity, 0);
                noOfPlayers = 2;
            }
            else if (PhotonNetwork.room.PlayerCount == 3 && noOfPlayers == 2)
            {
                PhotonNetwork.Instantiate(players[2].name, spawnLocation[1].transform.position, Quaternion.identity, 0);
                noOfPlayers = 3;
            }
            else if (PhotonNetwork.room.PlayerCount == 4 && noOfPlayers == 3)
            {
                PhotonNetwork.Instantiate(players[3].name, spawnLocation[1].transform.position, Quaternion.identity, 0);
                noOfPlayers = 4;
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

    /*void PlayerCheck()
    {
        noOfPlayers = PhotonNetwork.room.PlayerCount;

        for (int i = 0; i < noOfPlayers; i++)
        {
            if (noOfPlayers > 4)
            {
                noOfPlayers -= 4;
            }
        }
    }*/
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
