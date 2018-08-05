﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	#region Private Variables
	[SerializeField]
	private GameObject Player;

	private Vector3 Offset;
	#endregion
	
	void Awake()
	{
		Player = GameObject.FindGameObjectWithTag("Player");
	}
	void Start () 
	{
		Offset = transform.position - Player.transform.position;
	}	
	void LateUpdate () 
	{
		transform.position = Player.transform.position + Offset;
	}
}
