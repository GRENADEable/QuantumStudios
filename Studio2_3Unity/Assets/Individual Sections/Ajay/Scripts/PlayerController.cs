using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Photon.PunBehaviour
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
    private PhotonView pview;
    private Vector3 tarPos;
    private Quaternion tarRot;
    [SerializeField]
    private float movementValue = 0.25f; //Default 0.25f
    [SerializeField]
    private float rotateValue = 500f; //Default 500f
    #endregion

    #region Callbacks
    void Start()
    {
        MyRB = GetComponent<Rigidbody>();
        pview = GetComponent<PhotonView>();
    }

    void Update()
    {
        if (pview.isMine)
            MovePlayer();
        else
            SmoothMovement();
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
    #endregion

    #region My Functions
    void MovePlayer()
    {
        float MoveHorizontal = Input.GetAxisRaw("Horizontal");
        float MoveVertical = Input.GetAxisRaw("Vertical");

        MovementInput = new Vector3(MoveHorizontal, 0.0f, MoveVertical);
        MyRB.rotation = Quaternion.Slerp(MyRB.rotation,Quaternion.LookRotation(MovementInput), 0.15f);

        MovementInput = Vector3.ClampMagnitude(MovementInput, clampMax);
        MyRB.AddForce (MovementInput * MoveSpeed);
    }

    void SmoothMovement()
    {
        transform.position = Vector3.Lerp(transform.position, tarPos, movementValue);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, tarRot, rotateValue * Time.deltaTime);
    }
    #endregion

    #region Photon Callbacks
    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
        }
        else
        {
            tarPos = (Vector3)stream.ReceiveNext();
            tarRot = (Quaternion)stream.ReceiveNext();
        }
    }
    #endregion

}

