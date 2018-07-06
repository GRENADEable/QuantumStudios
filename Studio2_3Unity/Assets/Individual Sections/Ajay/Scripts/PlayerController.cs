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
    private Vector3 MovementVelocity;
    #endregion

    void Start()
    {
        MyRB = GetComponent<Rigidbody>();
    }


    void Update()
    {
        MovementInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        MovementVelocity = MovementInput * MoveSpeed;
    }

    void FixedUpdate()
    {
        MyRB.velocity = MovementVelocity;
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

