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
    public GameObject myPlayer1;
    public GameObject myPlayer2;
    public GameObject myPlayer3;
    public GameObject myPlayer4;
    [Header("Score")]
    public Text scoreText;
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
                GameObject myPlayerObj = PhotonNetwork.Instantiate(player1.name, playerSpawnLocation[index].transform.position, Quaternion.identity, 0);
                myPlayer1 = myPlayerObj;

                player1 = GameObject.FindGameObjectWithTag("Player1");
                Instantiate(cam, player1.transform.position + camOffset, Quaternion.Euler(45.0f, 0f, 0f));
            }

            if (player2 != null && PhotonNetwork.room.PlayerCount == 2)
            {
                GameObject myPlayerObj = PhotonNetwork.Instantiate(player2.name, playerSpawnLocation[index].transform.position, Quaternion.identity, 0);
                myPlayer2 = myPlayerObj;

                player2 = GameObject.FindGameObjectWithTag("Player2");
                Instantiate(cam, player2.transform.position + camOffset, Quaternion.Euler(45.0f, 0f, 0f));
            }

            if (player3 != null && PhotonNetwork.room.PlayerCount == 3)
            {
                GameObject myPlayerObj = PhotonNetwork.Instantiate(player3.name, playerSpawnLocation[index].transform.position, Quaternion.identity, 0);
                myPlayer3 = myPlayerObj;
                player3 = GameObject.FindGameObjectWithTag("Player3");
                Instantiate(cam, player3.transform.position + camOffset, Quaternion.Euler(45.0f, 0f, 0f));
            }

            if (player4 != null && PhotonNetwork.room.PlayerCount == 4)
            {
                GameObject myPlayerObj = PhotonNetwork.Instantiate(player4.name, playerSpawnLocation[index].transform.position, Quaternion.identity, 0);
                myPlayer4 = myPlayerObj;

                player4 = GameObject.FindGameObjectWithTag("Player4");
                Instantiate(cam, player4.transform.position + camOffset, Quaternion.Euler(45.0f, 0f, 0f));
            }
        }
    }

    void Update()
    {
        if (myPlayer1 != null)
            scoreText.text = myPlayer1.GetComponent<PlayerController>().score.ToString();

        if (myPlayer2 != null)
            scoreText.text = myPlayer2.GetComponent<PlayerController>().score.ToString();

        if (myPlayer3 != null)
            scoreText.text = myPlayer3.GetComponent<PlayerController>().score.ToString();

        if (myPlayer4 != null)
            scoreText.text = myPlayer4.GetComponent<PlayerController>().score.ToString();
    }
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
