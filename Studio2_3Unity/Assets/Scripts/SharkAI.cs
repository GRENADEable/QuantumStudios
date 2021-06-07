using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SharkAI : MonoBehaviour
{
    #region Public Variables
    public float maxSpeed;
    public float maxForce;
    public float minDis;
    public float distance;
    public float waitTime;
    [SerializeField]
    //private SharkSpawning AI;
    //public float spaceBetween;
    #endregion

    #region Private Variables
    private Rigidbody sharkRB;
    [SerializeField]
    private GameObject Player;
    [SerializeField]
    private Animator anim;
    private Animator playerAnim;
    #endregion


    void Start()
    {
        sharkRB = GetComponent<Rigidbody>();
        //AI = GameObject.FindGameObjectWithTag("Spawner").GetComponent<SharkSpawning>();
        Player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();
        playerAnim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
    }

    void Update()
    {
        distance = Vector3.Distance(Player.transform.position, transform.position);
        if (distance <= minDis)
        {
            anim.SetBool("isNear", true);
            StartCoroutine(destroyDelay());

        }
        else
        {
            anim.SetBool("isNear", false);
        }
    }
    void FixedUpdate()
    {
        Vector3 headDir = (new Vector3(Player.gameObject.transform.position.x, 0, Player.gameObject.transform.position.z) - new Vector3(this.gameObject.transform.position.x, 0, this.gameObject.transform.position.z)).normalized;

        Vector3 desiredVelocity = (Player.transform.position - transform.position).normalized * maxSpeed;
        Vector3 steering = desiredVelocity - sharkRB.velocity;
        Vector3 clampSteering = Vector3.ClampMagnitude(steering, maxForce);

        sharkRB.AddForce(clampSteering, ForceMode.Impulse);
        transform.LookAt(transform.position + sharkRB.velocity);

        //Add seperation between each AI
        /*foreach (GameObject go in AI.sharkAndroidPrefab)
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
        }*/
    }
    IEnumerator destroyDelay()
    {
        yield return new WaitForSeconds(waitTime);
        playerAnim.SetBool("isDead", true);
    }
}
