using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buoyancy : MonoBehaviour
{
    #region Public Variables
    public float buoyancy = 20.0f;
    public float viscosity;
    #endregion

    #region Private Variables
    private Rigidbody rg;
    #endregion

    #region Unity Calbacks
    void Awake()
    {
        rg = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Vector3[] vertices = WaterDeformation.mesh.vertices;
        Vector3[] worldVertices = new Vector3[vertices.Length];

        for (int i = 0; i < vertices.Length; i++)
        {
            worldVertices[i] = WaterDeformation.water.TransformPoint(vertices[i]);
        }

        Vector3 nearestVertices = NearVertices(transform.position, worldVertices);

        if (transform.position.y < nearestVertices.y)
        {
            rg.AddForce(Vector3.up * buoyancy);
            rg.velocity /= ((viscosity / 100) + 1);
        }
    }
    #endregion

    #region My Functions
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
    #endregion
}
