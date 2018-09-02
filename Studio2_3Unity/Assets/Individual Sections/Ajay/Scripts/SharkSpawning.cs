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
    private int sharkCount = 0;
    [SerializeField]
    private GameObject shark;
    [SerializeField]
    private float sharkTimer;
    [SerializeField]
    private float spawnTime;
    [SerializeField]
    private int sharkLimit;
    [SerializeField]
    private int index;
    [SerializeField]
    private GameObject[] sharks;
    public List<Rigidbody> boids;
    [SerializeField]
    private List<GameObject> sharkObj;
    #endregion

    void Awake()
    {
        sharkSpawnLocation = GameObject.FindGameObjectsWithTag("SharkSpawner");
        boids = new List<Rigidbody>();
        Spawn();
        sharkTimer = spawnTime;
    }

    void FixedUpdate()
    {
        sharkTimer -= Time.deltaTime;
        index = Random.Range(0, sharkSpawnLocation.Length);

#if UNITY_EDITOR || UNITY_STANDALONE
        if (sharkTimer <= 0)
        {
            sharkTimer = spawnTime;
            ActivateEnemy();
            Debug.LogWarning("Spawning Shark for PC Build");
        }
#else
        if (sharkTimer <= 0 && sharkCount <= sharkLimit)
        {
            index = Random.Range(0, sharkSpawnLocation.Length);
            Instantiate(shark, sharkSpawnLocation[index].transform.position, Quaternion.identity);
            sharkTimer = spawnTime;
            sharkCount++;
        }
#endif
    }
    void Spawn()
    {
        for (int i = 0; i < sharkSpawnLocation.Length; i++)
        {
            GameObject enemy = Instantiate(shark, sharkSpawnLocation[i].transform.position, Quaternion.identity);
            SharkBoids();
            enemy.SetActive(false);
            sharkObj.Add(enemy);
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
