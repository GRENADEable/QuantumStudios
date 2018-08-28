using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkSeeking : MonoBehaviour
{
	public float maxSpeed;
	public float maxForce;
    private GameObject Player;
	private Rigidbody sharkRB;
    void Start()
    {
		sharkRB = GetComponent<Rigidbody>();
        Player = GameObject.FindGameObjectWithTag("Player");
    }


    void FixedUpdate()
    {
        Vector3 desiredVelocity = (Player.transform.position - transform.position).normalized * maxSpeed;
        Vector3 steering = desiredVelocity - sharkRB.velocity;
        Vector3 clampSteering = Vector3.ClampMagnitude(steering, maxForce);
        sharkRB.AddForce(clampSteering);

        transform.LookAt(transform.position + sharkRB.velocity);
    }

}
