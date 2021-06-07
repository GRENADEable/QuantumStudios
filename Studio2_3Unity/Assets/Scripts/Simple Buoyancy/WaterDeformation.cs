using System.Collections;
using UnityEngine;

public class WaterDeformation : MonoBehaviour
{
    #region Public Variables
    public static Mesh mesh;
    public static Transform water;
    #endregion


    #region Unity Calbacks
    void Start()
    {
        water = transform;
        mesh = GetComponent<MeshFilter>().mesh;
    }
    void FixedUpdate()
    {
        Vector3[] vertices = mesh.vertices;

        mesh.vertices = vertices;
        mesh.RecalculateNormals();

        GetComponent<MeshFilter>().mesh = mesh;
    }
    #endregion
}
