using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawner : Photon.MonoBehaviour
{
    #region Public Variables
    public float maxTime;
    public float timer = 0;
    #endregion

    #region Private Variables
    [SerializeField]
    private GameObject[] powerupSpawnLocation;
    [SerializeField]
    private string index;
    [SerializeField]
    private int offlineIndex;
    [SerializeField]
    private GameObject powerUp;
    [SerializeField]
    private List<GameObject> powerPickUp;
    #endregion

    #region Unity Callbacks
    void Awake()
    {
        if (PhotonNetwork.connected && PhotonNetwork.isMasterClient)
        {
            timer = maxTime;
            powerupSpawnLocation = GameObject.FindGameObjectsWithTag("SpeedPowerSpawn");

            for (int i = 0; i < powerupSpawnLocation.Length; i++)
            {
                GameObject obj = PhotonNetwork.Instantiate(powerUp.name, powerupSpawnLocation[i].transform.position, Quaternion.identity, 0);
                powerUp.SetActive(false);
                powerPickUp.Add(obj);
            }
        }
        else
        {
            timer = maxTime;
            powerupSpawnLocation = GameObject.FindGameObjectsWithTag("SpeedPowerSpawn");
            for (int i = 0; i < powerupSpawnLocation.Length; i++)
            {
                GameObject obj = Instantiate(powerUp, powerupSpawnLocation[i].transform.position, Quaternion.identity);
                powerUp.SetActive(false);
                powerPickUp.Add(obj);
            }
        }
    }
    void FixedUpdate()
    {
        if (PhotonNetwork.isMasterClient && PhotonNetwork.connected)
        {
            index = Random.Range(0, powerPickUp.Count).ToString();
            timer -= Time.deltaTime;
            if (timer <= 0f)
            {
                this.photonView.RPC("Spawner", PhotonTargets.All, index);
                timer = maxTime;
            }
        }
        else
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
        Debug.LogWarning("Spawning SpeedPowerup");
        int convertIndex = int.Parse(intConvert);
        for (int i = 0; i < powerPickUp.Count; i++)
        {
            if (!powerPickUp[i].activeInHierarchy)
            {
                powerPickUp[convertIndex].SetActive(true);
                break;
            }
        }
    }

    void OfflineSpawner()
    {
        if (!powerPickUp[offlineIndex].activeInHierarchy)
        {
            powerPickUp[offlineIndex].SetActive(true);
        }
    }
    #endregion
}
