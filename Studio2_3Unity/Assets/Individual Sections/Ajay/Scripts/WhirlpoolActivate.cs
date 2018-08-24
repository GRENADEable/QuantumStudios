using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhirlpoolActivate : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetKey (KeyCode.E))
			{
				transform.localScale = new Vector3(3f,3f,0.04683586f);
			}
	}
	
	
}
