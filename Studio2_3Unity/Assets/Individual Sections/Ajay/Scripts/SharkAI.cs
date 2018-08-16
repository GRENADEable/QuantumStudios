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
    GameObject[] AI;
    public float spaceBetween;
    public AudioClip gameOverSoundFX;
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


    void Update()
    {
        Vector3 headDir = (new Vector3 (Player.gameObject.transform.position.x, 0, Player.gameObject.transform.position.z) - new Vector3(this.gameObject.transform.position.x, 0, this.gameObject.transform.position.z)).normalized;
        
        moveSpeed = Mathf.Clamp(moveSpeed,0,5);
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

        
        //transform.LookAt(Player.transform.position);

        //if (Vector3.Distance(transform.position, Player.transform.position) >= minDis && Player != null)
        //{
        //    transform.position += transform.forward * moveSpeed * Time.deltaTime;
        //}

    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            AudioManager.instance.PlaySingle(gameOverSoundFX);
            AudioManager.instance.musicSource.Stop();
            //Destroy(other.gameObject);
            SceneManager.LoadScene("GameOverScene");

        }
    }

}
