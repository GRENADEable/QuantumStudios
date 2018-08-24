using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniSharkAI : Photon.MonoBehaviour
{

    #region Public Variables
    public float moveSpeed;
    public float maxSpeed;
    public float buoyancy = 20.0f;
    public float viscosity;
    #endregion

    #region Private Variables
    private Rigidbody minisharkRB;
    [SerializeField]
    private GameObject[] targets;
    [SerializeField]
    private GameObject target;
    private Vector3 tarPos;
    private Quaternion tarRot;
    [SerializeField]
    private float movementValue = 0.25f; //Default 0.25f
    [SerializeField]
    private float rotateValue = 500f; //Default 500f
    #endregion

    #region Unity Callbacks
    void Start()
    {
        minisharkRB = GetComponent<Rigidbody>();
    }

    void OnEnable()
    {
        targets = GameObject.FindGameObjectsWithTag("Player");

        if (PhotonNetwork.isMasterClient)
        {
            int index = Random.Range(0, targets.Length);
            this.photonView.RPC("FollowPlayer", PhotonTargets.AllViaServer, index.ToString());
        }
    }

    void FixedUpdate()
    {
        if (targets != null)
        {
            Vector3 headDir = (new Vector3(target.transform.position.x, 0, target.transform.position.z) - new Vector3(this.gameObject.transform.position.x, 0, this.gameObject.transform.position.z)).normalized;

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
    }


    #endregion

    #region Photon Callbacks
    /*void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
        }
        else
        {
            tarPos = (Vector3)stream.ReceiveNext();
            tarRot = (Quaternion)stream.ReceiveNext();
        }
    }*/

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

    /*void SmoothMovement()
    {
        transform.position = Vector3.Lerp(transform.position, tarPos, movementValue);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, tarRot, rotateValue * Time.deltaTime);
    }*/

    [PunRPC]
    public void FollowPlayer(string stringToInt)
    {
        int convertedIndex = int.Parse(stringToInt);
        target = targets[convertedIndex];
    }
    #endregion
}
