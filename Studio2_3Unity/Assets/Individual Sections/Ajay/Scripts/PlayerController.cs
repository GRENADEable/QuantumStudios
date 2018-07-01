using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float MoveSpeed;
	private Rigidbody MyRB;
	private Vector3 MovementInput;
	private Vector3 MovementVelocity;

	void Start () 
	{
		MyRB = GetComponent<Rigidbody>();
	}
	
	
	void Update () 
	{
		MovementInput = new Vector3 (Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
		MovementVelocity = MovementInput * MoveSpeed;
	}
	
	void FixedUpdate()
	{
		MyRB.velocity = MovementVelocity;
	}
}
