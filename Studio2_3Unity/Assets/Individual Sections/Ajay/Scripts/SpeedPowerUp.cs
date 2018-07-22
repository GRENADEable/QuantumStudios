using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPowerUp : MonoBehaviour
{
    #region Public Variables
    public GameObject PickUpFX;
    public float Duration = 3f;
    public float Multiplier = 2f;
    #endregion

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            StartCoroutine(PickUp(other));
        }
    }

    IEnumerator PickUp(Collider player)
    {
        Instantiate(PickUpFX, transform.position, transform.rotation);

        PlayerControllerSinglePlayer ply = player.GetComponent<PlayerControllerSinglePlayer>();
        ply.moveSpeed *= Multiplier;

        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;

        yield return new WaitForSeconds(Duration);

        ply.moveSpeed /= Multiplier;
        Destroy(gameObject);
    }
    void Start()
    {

    }


    void Update()
    {

    }
}
