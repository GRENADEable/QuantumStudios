using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SharkAI : MonoBehaviour
{

    #region Public Variables
    public Transform Player;
    public int moveSpeed;
    public int maxDis;
    public int minDis;
    GameObject[] AI;
    public float spaceBetween;
    public AudioClip gameOverSoundFX;
    #endregion

    #region Private Variables
    private Rigidbody sharkRB;
    #endregion


    void Start()
    {
        sharkRB = GetComponent<Rigidbody>();
        AI = GameObject.FindGameObjectsWithTag("AIShark");
    }


    void Update()
    {
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

        //Look at the player and start moving towards them
        transform.LookAt(Player);

        if (Vector3.Distance(transform.position, Player.position) >= minDis)
        {
            transform.position += transform.forward * moveSpeed * Time.deltaTime;
        }

    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            AudioManager.instance.PlaySingle(gameOverSoundFX);
            AudioManager.instance.musicSource.Stop();
            Destroy(other.gameObject);
            SceneManager.LoadScene("GameOverScene");

        }
    }

}
