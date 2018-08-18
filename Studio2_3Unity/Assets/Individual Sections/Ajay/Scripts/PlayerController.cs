using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : Photon.PunBehaviour
{
    #region Public Variables
    public float moveSpeed;
    public float slowSpeed;
    public float regularSpeed;
    public float clampMax;
    public float powerUpSpeed;

    //public GameObject pickUpFX;
    public float spDuration;
    #endregion

    #region Private Variables
    private Rigidbody myRB;
    private Vector3 movementInput;
    private PhotonView pview;
    private Vector3 tarPos;
    private Quaternion tarRot;
    [SerializeField]
    private float movementValue = 0.25f; //Default 0.25f
    [SerializeField]
    private float rotateValue = 500f; //Default 500f
    [SerializeField]
    private double timer;
    [SerializeField]
    private MobileJoystick mobileJoy;
    private GameObject mobilePrefab;
    private CameraFollow cam;
    private UIManagerOnline minimapCam;
    [SerializeField]
    private Text playerName;
    //private Text score;
    #endregion

    #region Callbacks
    void Start()
    {
        myRB = GetComponent<Rigidbody>();
        pview = GetComponent<PhotonView>();

        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFollow>();
        mobileJoy = GameObject.FindGameObjectWithTag("Joystick").GetComponent<MobileJoystick>();

        mobilePrefab = GameObject.FindGameObjectWithTag("Joystick");
        minimapCam = GameObject.FindGameObjectWithTag("MinimapCamera").GetComponent<UIManagerOnline>();

        //score = GameObject.FindGameObjectWithTag("Score").GetComponent<Text>();
        playerName = GameObject.FindGameObjectWithTag("PlayerText").GetComponent<Text>();
        SetName();

        if (pview.isMine)
        {
            cam.Player = this.gameObject;
            minimapCam.player = this.gameObject;
        }

    }

    void FixedUpdate()
    {
        if (pview.isMine)
            MovePlayer();
        else
            SmoothMovement();

        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            moveSpeed = regularSpeed;
            timer = 5;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "SpeedPowerUp")
        {
            moveSpeed = powerUpSpeed;
            other.gameObject.SetActive(false);
            timer = spDuration;
        }

        if (other.tag == "Whirlpool")
        {
            //score.text = 0.ToString();
            this.gameObject.SetActive(false);
            this.gameObject.transform.position = new Vector3(115.0f, 1.35f, 80.0f);
            this.gameObject.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        moveSpeed = regularSpeed;
    }
    #endregion

    #region My Functions
    void MovePlayer()
    {
#if UNITY_EDITOR || UNITY_STANDALONE
        float MoveHorizontal = Input.GetAxisRaw("Horizontal");
        float MoveVertical = Input.GetAxisRaw("Vertical");
        mobilePrefab.SetActive(false);
#else
        float MoveHorizontal = mobileJoy.Horizontal();
        float MoveVertical = mobileJoy.Vertical();
        mobilePrefab.SetActive(true);
#endif

        movementInput = new Vector3(MoveHorizontal, 0.0f, MoveVertical);

        if (movementInput != Vector3.zero)
        {
            myRB.rotation = Quaternion.Slerp(myRB.rotation, Quaternion.LookRotation(movementInput), 0.15f);

            movementInput = Vector3.ClampMagnitude(movementInput, clampMax);
            myRB.AddForce(movementInput * moveSpeed, ForceMode.Impulse);
        }
    }

    void SmoothMovement()
    {
        transform.position = Vector3.Lerp(transform.position, tarPos, movementValue);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, tarRot, rotateValue * Time.deltaTime);
    }

    public void SetName()
    {
        if (playerName != null)
        {
            playerName.text = PhotonNetwork.player.NickName;
        }
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

