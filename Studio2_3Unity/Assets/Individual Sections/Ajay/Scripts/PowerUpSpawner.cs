using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawner : Photon.MonoBehaviour
{
    #region Public Variables
    public float maxTime;
    public float maxTimeOffline;
    public float timer = 0;
    #endregion

    #region Private Variables
    [SerializeField]
    private GameObject[] powerupSpawnLocation;
    [SerializeField]
    private GameObject[] sharkPowerupSpawnLocation;
    [SerializeField]
    private string index;
    [SerializeField]
    private string indexShark;
    [SerializeField]
    private int offlineIndex;
    [SerializeField]
    private GameObject speedPowerUp;
    [SerializeField]
    private GameObject sharkPowerUp;
    [SerializeField]
    private List<GameObject> powerPickUp;
    [SerializeField]
    private List<GameObject> sharkPickup;
    #endregion

    #region Unity Callbacks
    void Awake()
    {
        //Debug.LogWarning(this.gameObject.name + " is Master");
        maxTime = 15.0f;
        timer = maxTime;
        powerupSpawnLocation = GameObject.FindGameObjectsWithTag("PowerUpSpawn");

        for (int i = 0; i < powerupSpawnLocation.Length; i++)
        {
            GameObject speedObj = PhotonNetwork.Instantiate(speedPowerUp.name, powerupSpawnLocation[i].transform.position, Quaternion.identity, 0);
            speedPowerUp.SetActive(false);
            powerPickUp.Add(speedObj);
            Debug.LogWarning("Adding Speed Powerup to List for Online");
        }

        sharkPowerupSpawnLocation = GameObject.FindGameObjectsWithTag("SharkPowerUpSpawn");
        for (int i = 0; i < sharkPowerupSpawnLocation.Length; i++)
        {
            GameObject miniSharkObj = PhotonNetwork.Instantiate(sharkPowerUp.name, sharkPowerupSpawnLocation[i].transform.position, Quaternion.identity, 0);
            miniSharkObj.SetActive(false);
            sharkPickup.Add(miniSharkObj);
            Debug.LogWarning("Adding Shark Powerup to List for Online");
        }

        if (!PhotonNetwork.connected)
        {
            timer = maxTimeOffline;
            powerupSpawnLocation = GameObject.FindGameObjectsWithTag("SpeedPowerSpawn");
            for (int i = 0; i < powerupSpawnLocation.Length; i++)
            {
                GameObject obj = Instantiate(speedPowerUp, powerupSpawnLocation[i].transform.position, powerupSpawnLocation[i].transform.rotation);
                speedPowerUp.SetActive(false);
                powerPickUp.Add(obj);
                Debug.LogWarning("Adding Speed Powerup to List for Offline");
            }
        }
    }
    void FixedUpdate()
    {
        if (PhotonNetwork.isMasterClient)
        {
            index = Random.Range(0, powerPickUp.Count).ToString();
            timer -= Time.deltaTime;
            if (timer <= 0f)
            {
                this.photonView.RPC("Spawner", PhotonTargets.All, index);
                timer = maxTime;
            }

            indexShark = Random.Range(0, sharkPickup.Count).ToString();
            if (timer <= 5f)
            {
                this.photonView.RPC("SharkSpawner", PhotonTargets.All, index);
                timer = maxTime;
            }
        }
        else if (!PhotonNetwork.connected)
        {
            offlineIndex = Random.Range(0, powerPickUp.Count);
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                OfflineSpawner();
                timer = maxTime;
            }
        }
    }
    #endregion

    #region My Functions
    [PunRPC]
    void Spawner(string intConvert)
    {
        int convertIndex = int.Parse(intConvert);
        for (int i = 0; i < powerPickUp.Count; i++)
        {
            if (!powerPickUp[i].activeInHierarchy)
            {
                powerPickUp[convertIndex].SetActive(true);
                Debug.LogWarning("Spawning Speed Power Up for Online");
                break;
            }
        }
    }

    [PunRPC]
    void SharkSpawner(string intConvert)
    {
        int convertIndex = int.Parse(intConvert);
        for (int i = 0; i < sharkPickup.Count; i++)
        {
            if (!sharkPickup[i].activeInHierarchy)
            {
                sharkPickup[convertIndex].SetActive(true);
                Debug.LogWarning("Spawning Shark Power Up for Online");
                break;
            }
        }
    }

    void OfflineSpawner()
    {
        if (!powerPickUp[offlineIndex].activeInHierarchy)
        {
            powerPickUp[offlineIndex].SetActive(true);
            Debug.LogWarning("Spawning Speed Power Up for Offline");
        }
    }
    #endregion
}
