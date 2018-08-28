using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkSpawner : MonoBehaviour
{
    #region Public Variables
    public int index;
    public int sharkCount = 0;
    public GameObject[] sharkSpawnLocation;
    //public GameObject shark;
    #endregion

    #region Private Variables
    #endregion
    void Start()
    {
        sharkSpawnLocation = GameObject.FindGameObjectsWithTag("SharkSpawner");
    }

    /*void OnTriggerEnter(Collider other)
    {
        index = Random.Range(0, sharkSpawnLocation.Length);

        if (other.gameObject.tag == "Player" && sharkCount <= 3)
        {
            Instantiate(shark, sharkSpawnLocation[index].transform.position, Quaternion.identity);
        }
    }*/

    /*void FixedUpdate()
    {
        index = Random.Range(0, sharkSpawnLocation.Length);
    }*/
}
