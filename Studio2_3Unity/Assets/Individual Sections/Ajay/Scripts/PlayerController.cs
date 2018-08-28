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
    public float buoyancy = 20.0f;
    public float viscosity;
    public float spDuration;
    //public PlayerNameObj plyNames;
    public GameObject miniShark;
    //public Text userName;

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
    [SerializeField]
    private GameObject mobilePrefab;
    private CameraFollow cam;
    private UIManagerOnline minimapCam;
    private Animator anim;
    [SerializeField]
    private bool hasSharkSeekPowerUp = false;
    //private Text playerName;
    #endregion

    #region Callbacks
    void Start()
    {
        myRB = GetComponent<Rigidbody>();
        pview = GetComponent<PhotonView>();

        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFollow>();
        mobilePrefab = GameObject.FindGameObjectWithTag("Joystick");
        if (mobileJoy != null)
            mobileJoy = GameObject.FindGameObjectWithTag("Joystick").GetComponent<MobileJoystick>();

        minimapCam = GameObject.FindGameObjectWithTag("MinimapCamera").GetComponent<UIManagerOnline>();

        anim = GetComponent<Animator>();
        //this.gameObject.SetActive(true);
        //score = GameObject.FindGameObjectWithTag("Score").GetComponent<Text>();
        //playerName = GameObject.FindGameObjectWithTag("PlayerText").GetComponent<Text>();
        //SetName();
        //this.pview.RPC("SetName", PhotonTargets.All, )
        //Sync();

        if (pview.isMine && this.tag == "Player1")
        {
            cam.player1 = this.gameObject;
            minimapCam.player = this.gameObject;
        }
        else if (pview.isMine && this.tag == "Player2")
        {
            cam.player2 = this.gameObject;
            minimapCam.player = this.gameObject;
        }
    }
    void Update()
    {
        if (hasSharkSeekPowerUp == true && Input.GetKeyDown(KeyCode.E) && pview.isMine)
        {
            PhotonNetwork.Instantiate(miniShark.name, myRB.position, myRB.rotation, 0);
            hasSharkSeekPowerUp = false;
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

        if (moveSpeed == regularSpeed)
            anim.SetBool("isFast", false);

        Vector3[] vertices = WaterDeformation.mesh.vertices;
        Vector3[] worldVertices = new Vector3[vertices.Length];

        for (int i = 0; i < vertices.Length; i++)
        {
            worldVertices[i] = WaterDeformation.water.TransformPoint(vertices[i]);
        }

        Vector3 nearestVertices = NearVertices(transform.position, worldVertices);

        if (transform.position.y < nearestVertices.y)
        {
            myRB.AddForce(Vector3.up * buoyancy);
            myRB.velocity /= ((viscosity / 100) + 1);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "SpeedPowerUp")
        {
            moveSpeed = powerUpSpeed;
            if (moveSpeed == powerUpSpeed)
                anim.SetBool("isFast", true);

            other.gameObject.SetActive(false);
            timer = spDuration;
        }

        if (other.tag == "Whirlpool")
        {
            this.gameObject.SetActive(false);
            this.gameObject.transform.position = GameManager.instance.spawnLocation[GameManager.instance.index].transform.position;
            this.gameObject.SetActive(true);
        }

        if (other.tag == "SharkSeekPowerUp")
        {
            hasSharkSeekPowerUp = true;
            other.gameObject.SetActive(false);
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
            anim.SetBool("isMoving", true);
        }
        else
            anim.SetBool("isMoving", false);
    }

    void SmoothMovement()
    {
        transform.position = Vector3.Lerp(transform.position, tarPos, movementValue);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, tarRot, rotateValue * Time.deltaTime);
    }

    /*public void SetName()
    {
        if (playerName != null)
            playerName.text = PhotonNetwork.player.NickName;
    }*/

    Vector3 NearVertices(Vector3 position, Vector3[] vertices)
    {
        Vector3 nearestVertices = Vector3.zero;

        float minimumDistance = 100;

        for (int i = 0; i < vertices.Length; i++)
        {
            if (Vector3.Distance(position, vertices[i]) < minimumDistance)
            {
                nearestVertices = vertices[i];
                minimumDistance = Vector3.Distance(position, vertices[i]);
            }
        }

        return nearestVertices;
    }
    #endregion

    #region Photon Callbacks
    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
            stream.SendNext(anim.GetBool("isMoving"));
            stream.SendNext(anim.GetBool("isFast"));
            //stream.SendNext(userName);
        }
        else
        {
            tarPos = (Vector3)stream.ReceiveNext();
            tarRot = (Quaternion)stream.ReceiveNext();
            this.anim.SetBool("isMoving", (bool)stream.ReceiveNext());
            this.anim.SetBool("isFast", (bool)stream.ReceiveNext());
            //userName.text = (string)stream.ReceiveNext();
        }
    }

    /*[PunRPC]
    public void Sync()
    {
        pview.RPC("DisplayPlayer", PhotonTargets.AllBuffered, new object[] { plyNames.uesrName });
    }

    [PunRPC]
    public void DisplayPlayer(string user)
    {
        userName.text = user;
    }*/
    #endregion

}

