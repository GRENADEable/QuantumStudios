using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canvas_Destroy : MonoBehaviour {

	
	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag =="Player")
		{
			Destroy(this.gameObject, 5f);
		}
	}
		
	}

