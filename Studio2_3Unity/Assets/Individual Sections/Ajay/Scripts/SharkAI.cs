using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SharkAI : MonoBehaviour
{

    #region Public Variables
    public int maxDis;
    public int minDis;
    public float maxSpeed;
    public float maxForce;
    [SerializeField]
    private GameObject[] AI;
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

        Vector3 desiredVelocity = (Player.transform.position - transform.position).normalized * maxSpeed;
        Vector3 steering = desiredVelocity - sharkRB.velocity;
        Vector3 clampSteering = Vector3.ClampMagnitude(steering, maxForce);

        sharkRB.AddForce(clampSteering, ForceMode.Impulse);
        transform.LookAt(transform.position + sharkRB.velocity);
        /*moveSpeed = Mathf.Clamp(moveSpeed, 0, maxSpeed);
        sharkRB.AddForce(headDir * moveSpeed, ForceMode.Impulse);

        //Look at the player and start moving towards them
        transform.LookAt(headDir + this.transform.position);*/

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
        //transform.LookAt(Player.transform.position);

        //if (Vector3.Distance(transform.position, Player.transform.position) >= minDis && Player != null)
        //{
        //    transform.position += transform.forward * moveSpeed * Time.deltaTime;
        //}

    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            SceneManagement.instance.GameOver();
            AudioManager.instance.StopAudio();
            AudioManager.instance.DeathAudio();
        }
    }
}
