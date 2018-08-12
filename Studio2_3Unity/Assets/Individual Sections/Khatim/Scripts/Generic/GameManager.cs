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
    public float timerCount = 0f;
    #endregion

    #region  Private Variables
    [SerializeField]
    private Text scoring;
    private Text timerSecond;
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
        if (plyPrefab != null)
        {
            Debug.Log("Spawning Player From: " + SceneManager.GetActiveScene().name);
            PhotonNetwork.Instantiate(this.plyPrefab.name, new Vector3(1.3f, 1f, 15.0f), Quaternion.identity, 0);
        }
        scoring = GameObject.FindGameObjectWithTag("Score").GetComponent<Text>();
        timerSecond = GameObject.FindGameObjectWithTag("Timer").GetComponent<Text>();
    }

    void Update()
    {
        scoreCount += pointsPerSec * Time.deltaTime;
        scoring.text = "Score: " + Mathf.Round(scoreCount);
        timerCount += Time.deltaTime;
		timerSecond.text = timerCount.ToString ("Time: 0");
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
