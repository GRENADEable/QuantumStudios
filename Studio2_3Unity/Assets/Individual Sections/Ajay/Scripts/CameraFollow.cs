using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	public GameObject Player;

	private Vector3 Offset;

	void Start () 
	{
		Offset = transform.position - Player.transform.position;
	}
	
	void Update () 
	{
		
	}

	
}
