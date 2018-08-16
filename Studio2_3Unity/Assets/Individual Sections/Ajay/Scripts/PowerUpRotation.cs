using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpRotation : MonoBehaviour {

	
	void Start () 
	{
		
	}
	
	
	void Update () 
	{
		transform.Rotate (new Vector3(0, Time.deltaTime * 50, 0));
	}
}
