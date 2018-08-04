using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkSpawner : MonoBehaviour {

	#region Public Variables
	public GameObject shark;
	public Transform sharkPos;
	#endregion
	void Start () 
	{
		
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == "Player")
		{
			Instantiate(shark,sharkPos.position,sharkPos.rotation);
		}
	}
	
	void Update () 
	{
		
	}
}
