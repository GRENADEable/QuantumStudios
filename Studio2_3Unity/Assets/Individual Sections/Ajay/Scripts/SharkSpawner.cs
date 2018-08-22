using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkSpawner : MonoBehaviour {

	#region Private Variables
	[SerializeField]
	private int index;
	[SerializeField]
	private GameObject[] sharkSpawnLocation;
	[SerializeField]
	private GameObject shark;
	#endregion
	void Start () 
	{
		sharkSpawnLocation = GameObject.FindGameObjectsWithTag("SharkSpawner");
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == "Player")
			{
				GameObject skp = Instantiate(shark,sharkSpawnLocation[index].transform.position,Quaternion.identity);
			}
	}
	
	void FixedUpdate () 
	{
		index = Random.Range(0, sharkSpawnLocation.Length);
	}
}
