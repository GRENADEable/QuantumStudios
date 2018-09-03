using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkSpawning : MonoBehaviour
{
    #region Private Variables
    [Header("Shark Spawning")]
    [SerializeField]
    private GameObject[] sharkSpawnLocation;
    [SerializeField]
    private GameObject[] sharkSpawnLocationAndroid;
    [Header("Shark Prefabs")]
    [SerializeField]
    private GameObject sharkPC;
    [SerializeField]
    private GameObject sharkAndroid;
    [Header("Shark Timers")]
    [SerializeField]
    private float sharkTimer;
    [SerializeField]
    private float spawnTime;
    [Header("Shark Spawn Indexes")]
    [SerializeField]
    private int index;
    [SerializeField]
    private int indexAndroid;
    [Header("Shark Spawn Limit")]
    [SerializeField]
    private int sharkLimit;
    [SerializeField]
    private int sharkCount;
    [Header("Shark Boids Count")]
    private GameObject[] sharks;
    [SerializeField]
    public GameObject[] sharkAndroidPrefab;
    public List<Rigidbody> boids;
    [SerializeField]
    private List<GameObject> sharkObj;
    #endregion

    void Awake()
    {
        sharkTimer = spawnTime;
#if UNITY_EDITOR || UNITY_STANDALONE
        sharkSpawnLocation = GameObject.FindGameObjectsWithTag("SharkSpawner");
        boids = new List<Rigidbody>();
        Spawn();
#else
        sharkSpawnLocationAndroid = GameObject.FindGameObjectsWithTag("SharkSpawnerAndroid");
#endif
    }

    void FixedUpdate()
    {
        sharkTimer -= Time.deltaTime;
        index = Random.Range(0, sharkSpawnLocation.Length);

#if UNITY_EDITOR || UNITY_STANDALONE
        //For PC
        if (sharkTimer <= 0)
        {
            sharkTimer = spawnTime;
            ActivateEnemy();
            Debug.LogWarning("Spawning Shark for PC Build");
        }
#else
        //For Android
        if (sharkTimer <= 0)
        {
            sharkTimer = spawnTime;
            SpawnAndroid();
            Debug.LogWarning("Spawning Shark for Android Build");
        }
#endif
    }
    void Spawn()
    {
        for (int i = 0; i < sharkSpawnLocation.Length; i++)
        {
            GameObject enemy = Instantiate(sharkPC, sharkSpawnLocation[i].transform.position, Quaternion.identity);
            SharkBoids();
            enemy.SetActive(false);
            sharkObj.Add(enemy);
        }
    }

    public void SpawnAndroid()
    {
        if (sharkCount <= sharkLimit)
        {
            indexAndroid = Random.Range(0, sharkSpawnLocationAndroid.Length);
            Instantiate(sharkAndroid, sharkSpawnLocationAndroid[indexAndroid].transform.position, Quaternion.identity);
            sharkCount++;
        }
    }
    void SharkBoids()
    {
        sharks = GameObject.FindGameObjectsWithTag("AIShark");

        for (int i = 0; i < sharks.Length; i++)
        {
            Rigidbody rbBoid = sharks[i].GetComponent<Rigidbody>();
            boids.Add(rbBoid);
        }
    }

    public void ActivateEnemy()
    {
        sharks = GameObject.FindGameObjectsWithTag("AIShark");

        if (!sharkObj[index].activeInHierarchy)
            sharkObj[index].SetActive(true);
    }
}
