using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementTest : MonoBehaviour
{
    public float moveSpeed;
    public float clampMax;
    private Rigidbody myRB;
    private Vector3 movementInput;
    private float MoveHorizontal;
    private float MoveVertical;
    private Animator anim;
    void Start()
    {
        myRB = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        MoveHorizontal = Input.GetAxisRaw("Horizontal");
        MoveVertical = Input.GetAxisRaw("Vertical");

        movementInput = new Vector3(MoveHorizontal, 0.0f, MoveVertical);
        if (movementInput != Vector3.zero)
        {
            myRB.rotation = Quaternion.Slerp(myRB.rotation, Quaternion.LookRotation(movementInput), 0.15f);

            movementInput = Vector3.ClampMagnitude(movementInput, clampMax);
            myRB.AddForce(movementInput * moveSpeed, ForceMode.Impulse);
            anim.SetBool("isMoving", true);
        }
        else
        {
            anim.SetBool("isMoving", false);
        }
    }
}
