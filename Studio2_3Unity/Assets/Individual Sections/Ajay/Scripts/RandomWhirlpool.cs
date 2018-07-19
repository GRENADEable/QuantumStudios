using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomWhirlpool : MonoBehaviour {

	
	void Start () 
	{
		
	}
	
	
	void Update()
	{
			
		{	
			transform.localScale = new Vector3(Random.Range(5,8), 0.3710543f ,Random.Range (1,6));
		}

		
	}
	
}
