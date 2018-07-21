using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerSinglePlayer : MonoBehaviour 
{
    #region Public Variables
    public float MoveSpeed;
    public float slowSpeed;
    public float regularSpeed;
    public float clampMax;
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
        
    }

    void FixedUpdate()
    {
        float MoveHorizontal = Input.GetAxisRaw("Horizontal");
        float MoveVertical = Input.GetAxisRaw("Vertical");

        MovementInput = new Vector3(MoveHorizontal, 0.0f, MoveVertical);
        MyRB.rotation = Quaternion.Slerp(MyRB.rotation,Quaternion.LookRotation(MovementInput), 0.15f);

        MovementInput = Vector3.ClampMagnitude(MovementInput, clampMax);
        MyRB.AddForce (MovementInput * MoveSpeed);
        
        //transform.Translate (MovementInput * MoveSpeed * Time.deltaTime, Space.World);
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
