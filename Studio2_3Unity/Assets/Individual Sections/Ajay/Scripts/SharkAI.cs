using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SharkAI : MonoBehaviour
{

    #region Public Variables
    public float moveSpeed;
    public int maxDis;
    public int minDis;

    public float maxSpeed;
    public float buoyancy = 20.0f;
    public float viscosity;
    GameObject[] AI;
    public float spaceBetween;
    #endregion

    #region Private Variables
    private Rigidbody sharkRB;
    [SerializeField]
    private GameObject Player;
    #endregion


    void Start()
    {
        sharkRB = GetComponent<Rigidbody>();
        AI = GameObject.FindGameObjectsWithTag("AIShark");
        Player = GameObject.FindGameObjectWithTag("Player");
    }


    void FixedUpdate()
    {
        Vector3 headDir = (new Vector3(Player.gameObject.transform.position.x, 0, Player.gameObject.transform.position.z) - new Vector3(this.gameObject.transform.position.x, 0, this.gameObject.transform.position.z)).normalized;

        moveSpeed = Mathf.Clamp(moveSpeed, 0, maxSpeed);
        sharkRB.AddForce(headDir * moveSpeed, ForceMode.Impulse);

        //Look at the player and start moving towards them
        transform.LookAt(headDir + this.transform.position);

        //Add seperation between each AI
        foreach (GameObject go in AI)
        {
            if (go != gameObject)
            {
                float dist = Vector3.Distance(go.transform.position, this.transform.position);
                if (dist <= spaceBetween)
                {
                    Vector3 direction = transform.position - go.transform.position;
                    transform.Translate(direction * Time.deltaTime);
                }
            }
        }

        Vector3[] vertices = WaterDeformation.mesh.vertices;
        Vector3[] worldVertices = new Vector3[vertices.Length];

        for (int i = 0; i < vertices.Length; i++)
        {
            worldVertices[i] = WaterDeformation.water.TransformPoint(vertices[i]);
        }

        Vector3 nearestVertices = NearVertices(transform.position, worldVertices);

        if (transform.position.y < nearestVertices.y)
        {
            sharkRB.AddForce(Vector3.up * buoyancy);
            sharkRB.velocity /= ((viscosity / 100) + 1);
        }


        //transform.LookAt(Player.transform.position);

        //if (Vector3.Distance(transform.position, Player.transform.position) >= minDis && Player != null)
        //{
        //    transform.position += transform.forward * moveSpeed * Time.deltaTime;
        //}

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
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            SceneManager.LoadScene("GameOverScene");
            AudioManager.instance.StopAudio();
            AudioManager.instance.AudioAccess(7);
            AudioManager.instance.AudioAccess(1);
            AudioManager.instance.AudioAccess(3);
            AudioManager.instance.AudioAccess(6);
        }
    }

}
