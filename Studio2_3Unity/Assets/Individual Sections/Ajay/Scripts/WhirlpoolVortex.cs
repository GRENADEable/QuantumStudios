using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhirlpoolVortex : MonoBehaviour {

	public float pullSpeed;
	private GameObject pulledObj;
	//private Rigidbody whirlpoolrb;

	void Start () 
	{
		//Rigidbody whirlpoolrb = gameObject.GetComponent<Rigidbody>();
	}
	
	
	void Update () 
	{
		
	}

	void OnTriggerStay(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			pulledObj = other.gameObject;
			pulledObj.transform.position = Vector3.MoveTowards(pulledObj.transform.position, this.transform.position, pullSpeed * Time.deltaTime);
		}
	}
}
