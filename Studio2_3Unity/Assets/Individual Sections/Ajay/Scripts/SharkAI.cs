using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SharkAI : MonoBehaviour {

	#region Public Variables
	public Transform Player;
	public int moveSpeed;
	public int maxDis;
	public int minDis;
	#endregion


	void Start () 
	{
		
	}
	
	
	void Update () 
	{
		transform.LookAt (Player);

		if (Vector3.Distance(transform.position, Player.position) >= minDis)
		{
			transform.position += transform.forward * moveSpeed * Time.deltaTime;

			
		}
	}
}
