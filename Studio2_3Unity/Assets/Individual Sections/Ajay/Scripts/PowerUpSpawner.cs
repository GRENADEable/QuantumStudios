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
        if (PhotonNetwork.connected && PhotonNetwork.isMasterClient)
        {
            timer = maxTime;
            powerupSpawnLocation = GameObject.FindGameObjectsWithTag("PowerUpSpawn");

            for (int i = 0; i < powerupSpawnLocation.Length; i++)
            {
                GameObject speedObj = PhotonNetwork.Instantiate(speedPowerUp.name, powerupSpawnLocation[i].transform.position, Quaternion.identity, 0);
                speedPowerUp.SetActive(false);
                powerPickUp.Add(speedObj);
            }

            sharkPowerupSpawnLocation = GameObject.FindGameObjectsWithTag("SharkPowerUpSpawn");
            for (int i = 0; i < sharkPowerupSpawnLocation.Length; i++)
            {
                GameObject miniSharkObj = PhotonNetwork.Instantiate(sharkPowerUp.name, sharkPowerupSpawnLocation[i].transform.position, Quaternion.identity, 0);
                miniSharkObj.SetActive(false);
                sharkPickup.Add(miniSharkObj);
            }
        }
        else
        {
            timer = maxTime;
            powerupSpawnLocation = GameObject.FindGameObjectsWithTag("SpeedPowerSpawn");
            for (int i = 0; i < powerupSpawnLocation.Length; i++)
            {
                GameObject obj = Instantiate(speedPowerUp, powerupSpawnLocation[i].transform.position, Quaternion.identity);
                speedPowerUp.SetActive(false);
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

            indexShark = Random.Range(0, sharkPickup.Count).ToString();
            if (timer <= 5f)
            {
                this.photonView.RPC("SharkSpawner", PhotonTargets.All, index);
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

     [PunRPC]
    void SharkSpawner(string intConvert)
    {
        Debug.LogWarning("Spawning SharkPowerup");
        int convertIndex = int.Parse(intConvert);
        for (int i = 0; i < sharkPickup.Count; i++)
        {
            if (!sharkPickup[i].activeInHierarchy)
            {
                sharkPickup[convertIndex].SetActive(true);
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
