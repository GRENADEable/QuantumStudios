using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerConrollerTest : MonoBehaviour
{
    #region Public Variables
    public float moveSpeed;
    public float clampMax;
    #endregion

    #region Private Variables
    private Rigidbody myRB;
    private Vector3 movementInput;
    #endregion

    #region Unity Callbacks
    void Start()
    {
        myRB = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        float MoveHorizontal = Input.GetAxisRaw("Horizontal");
        float MoveVertical = Input.GetAxisRaw("Vertical");

        movementInput = new Vector3(MoveHorizontal, 0.0f, MoveVertical);
        if (movementInput != Vector3.zero)
        {
            myRB.rotation = Quaternion.Slerp(myRB.rotation, Quaternion.LookRotation(movementInput), 0.15f);

            movementInput = Vector3.ClampMagnitude(movementInput, clampMax);
            myRB.AddForce(movementInput * moveSpeed, ForceMode.Impulse);
        }
    }
    #endregion
}
