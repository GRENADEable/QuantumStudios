using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VortexTest : MonoBehaviour
{
    #region Public Variables
    public float xSpeed = 1.0f;
    public bool manual = false;
    #endregion

    #region Private Variables

    #endregion

    #region Unity Callbacks
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!manual)
        {
            transform.RotateAround(transform.position, Vector3.up, xSpeed * Time.deltaTime);
        }
        else
        {
            if (Input.GetAxis("Horizontal") != 0)
            {
                transform.RotateAround(transform.position, Vector3.up, Input.GetAxis("Horizontal") * xSpeed * Time.deltaTime);
            }
        }
    }
    #endregion

    #region My Functions

    #endregion
}
