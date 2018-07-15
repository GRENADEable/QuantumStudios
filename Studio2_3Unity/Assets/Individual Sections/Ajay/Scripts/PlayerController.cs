using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Public Variables
    public float MoveSpeed;
    public float slowSpeed;
    public float regularSpeed;
    #endregion

    #region Private Variables
    private Rigidbody MyRB;
    private Vector3 MovementInput;
    #endregion

    void Start()
    {
        MyRB = GetComponent<Rigidbody>();
    }


    void Update()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        float MoveHorizontal = Input.GetAxisRaw("Horizontal");
        float MoveVertical = Input.GetAxisRaw("Vertical");

        MovementInput = new Vector3(MoveHorizontal, 0.0f, MoveVertical);
        transform.rotation = Quaternion.Slerp(transform.rotation,Quaternion.LookRotation(MovementInput), -90f);

        transform.Translate (MovementInput * MoveSpeed * Time.deltaTime, Space.World);
    }
    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Whirlpool")
        {
            MoveSpeed = slowSpeed;
        }
    }

    void OnTriggerExit(Collider other)
    {
        MoveSpeed = regularSpeed;
    }
}

