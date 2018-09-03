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
    [Header("Shark Boids Count")]
    private GameObject[] sharks;
    public List<Rigidbody> boids;
    [SerializeField]
    private List<GameObject> sharkObj;
    #endregion

    void Awake()
    {
        sharkSpawnLocation = GameObject.FindGameObjectsWithTag("SharkSpawner");
        sharkSpawnLocationAndroid = GameObject.FindGameObjectsWithTag("SharkSpawnerAndroid");
        boids = new List<Rigidbody>();
        Spawn();
        sharkTimer = spawnTime;
    }

    void FixedUpdate()
    {
        sharkTimer -= Time.deltaTime;
#if UNITY_EDITOR || UNITY_STANDALONE
        index = Random.Range(0, sharkSpawnLocation.Length);
#else
        indexAndroid = Random.Range(0, sharkSpawnLocationAndroid.Length);
#endif

#if UNITY_EDITOR || UNITY_STANDALONE
        if (sharkTimer <= 0)
        {
            sharkTimer = spawnTime;
            ActivateEnemy();
            Debug.LogWarning("Spawning Shark for PC Build");
        }
#else
        if (sharkTimer <= 0)
        {
            sharkTimer = spawnTime;
            ActivateEnemyAndroid();
            Debug.LogWarning("Spawning Shark for Android Build");
        }
#endif
    }
    void Spawn()
    {
#if UNITY_EDITOR || UNITY_STANDALONE
        for (int i = 0; i < sharkSpawnLocation.Length; i++)
        {
            GameObject enemy = Instantiate(sharkPC, sharkSpawnLocation[i].transform.position, Quaternion.identity);
            SharkBoids();
            enemy.SetActive(false);
            sharkObj.Add(enemy);
        }
#else
        for (int i = 0; i < sharkSpawnLocationAndroid.Length; i++)
        {
            GameObject enemy = Instantiate(sharkPC, sharkSpawnLocationAndroid[i].transform.position, Quaternion.identity);
            enemy.SetActive(false);
            sharkObj.Add(enemy);
        }
#endif
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

    public void ActivateEnemyAndroid()
    {
        if (!sharkObj[index].activeInHierarchy)
            sharkObj[index].SetActive(true);
    }
}
