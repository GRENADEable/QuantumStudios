using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniSharkAI : MonoBehaviour 
{

	#region Public Variables
	
	public float moveSpeed;
	public int maxDis;
	public int minDis;
	public float maxSpeed;
	public float buoyancy = 20.0f;
    public float viscosity;
	#endregion
	
	#region Private Variables
	private Rigidbody minisharkRB;
	private GameObject Target;
	#endregion

	void Start () 
	{
		minisharkRB = GetComponent<Rigidbody>();
		Target = GameObject.FindGameObjectWithTag("Player");
		
	}
	
	void FixedUpdate () 
	{
		Vector3 headDir = (new Vector3(Target.gameObject.transform.position.x, 0, Target.gameObject.transform.position.z) - new Vector3(this.gameObject.transform.position.x, 0, this.gameObject.transform.position.z)).normalized;

        moveSpeed = Mathf.Clamp(moveSpeed, 0, maxSpeed);
        minisharkRB.AddForce(headDir * moveSpeed, ForceMode.Impulse);

        //Look at the player and start moving towards them
        transform.LookAt(headDir + this.transform.position);

		 Vector3[] vertices = WaterDeformation.mesh.vertices;
        Vector3[] worldVertices = new Vector3[vertices.Length];

        for (int i = 0; i < vertices.Length; i++)
        {
            worldVertices[i] = WaterDeformation.water.TransformPoint(vertices[i]);
        }

        Vector3 nearestVertices = NearVertices(transform.position, worldVertices);

        if (transform.position.y < nearestVertices.y)
        {
            minisharkRB.AddForce(Vector3.up * buoyancy);
            minisharkRB.velocity /= ((viscosity / 100) + 1);
        }
	}
	Vector3 NearVertices(Vector3 position, Vector3[] vertices)
    {
        Vector3 nearestVertices = Vector3.zero;

        float minimumDistance = 100;

        for (int i = 0; i < vertices.Length; i++)
        {
            if (Vector3.Distance(position, vertices[i]) < minimumDistance)
            {
                nearestVertices = vertices[i];
                minimumDistance = Vector3.Distance(position, vertices[i]);
            }
        }

        return nearestVertices;
    }
}
