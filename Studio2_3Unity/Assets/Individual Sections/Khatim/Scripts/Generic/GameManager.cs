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
    public float scoreCount;
    public float pointsPerSec;
    #endregion

    #region  Private Variables
    [SerializeField]
    private Text scoring;
    //private GameObject[] speedPowers;
    #endregion

    #region Unity Callbacks
    void Awake()
    {
        if (instance == null)
            instance = this;

        else if (instance != null)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        if (plyPrefab != null || scoring != null)
        {
            Debug.Log("Spawning Player From: " + SceneManager.GetActiveScene().name);
            PhotonNetwork.Instantiate(this.plyPrefab.name, new Vector3(0f, 1f, 15.0f), Quaternion.identity, 0);

            //speedPowers = GameObject.FindGameObjectsWithTag("SpeedPowerUp");
            scoring = GameObject.FindGameObjectWithTag("Score").GetComponent<Text>();
        }
    }

    void Update()
    {
        if (scoring != null)
        {
            scoring.text = "Score: " + Mathf.Round(scoreCount);
            scoreCount += pointsPerSec * Time.deltaTime;
        }

        /*foreach (GameObject speed in speedPowers)
        {
            //Rotation of powerup
            speed.transform.Rotate(Vector3.right * Time.deltaTime);
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
