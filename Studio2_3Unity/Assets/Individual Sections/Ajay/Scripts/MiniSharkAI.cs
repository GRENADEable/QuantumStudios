using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniSharkAI : MonoBehaviour 
{

	#region Public Variables
	
	public int moveSpeed;
	public int maxDis;
	public int minDis;
	#endregion
	
	#region Private Variables
	private Rigidbody minisharkRB;
	private Transform Target;
	#endregion

	void Start () 
	{
		minisharkRB = GetComponent<Rigidbody>();
		Target = GameObject.FindWithTag("Target").transform;
		
	}
	
	void Update () 
	{
		transform.LookAt(Target);

		if (Vector3.Distance(transform.position, Target.position) >= minDis)
		{
			transform.position += transform.forward * moveSpeed * Time.deltaTime;
		}
	}
}
