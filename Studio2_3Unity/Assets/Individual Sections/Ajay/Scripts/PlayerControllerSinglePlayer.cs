using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControllerSinglePlayer : MonoBehaviour
{
    #region Public Variables
    public float moveSpeed;
    public float slowSpeed;
    public float regularSpeed;
    public float powerUpSpeed;
    public float clampMax;
    //public GameObject miniShark;
    //public GameObject pickUpFX2;
    public float spDuration;
    //public bool hasSharkSeekPowerUp;
    public AudioClip surfing;
    public AudioClip speedPUP;


    #endregion

    #region Private Variables
    private Rigidbody myRB;
    private Vector3 movementInput;
    [SerializeField]
    private float timer;
    [SerializeField]
    private MobileJoystick mobileJoy;
    private GameObject mobilePrefab;
    [SerializeField]
    private Animator anim;
    [SerializeField]
    private float MoveHorizontal;
    [SerializeField]
    private float MoveVertical;
    #endregion

    void Start()
    {
        myRB = GetComponent<Rigidbody>();
        mobilePrefab = GameObject.FindGameObjectWithTag("Joystick");
        mobileJoy = GameObject.FindGameObjectWithTag("Joystick").GetComponent<MobileJoystick>();
        anim = GetComponent<Animator>();
    }


    void Update()
    {
        //the problem here is that if I set the timer to <= 0, then the player will always stay at regular speed even through the whirlpool

        //if (hasSharkSeekPowerUp == true && Input.GetKeyDown(KeyCode.E))
        //{
        //    Instantiate(miniShark, myRB.position, myRB.rotation);
        //}
    }

    void FixedUpdate()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            moveSpeed = regularSpeed;
            timer = 5;
        }

        if (moveSpeed == regularSpeed)
            anim.SetBool("isFast", false);

#if UNITY_EDITOR || UNITY_STANDALONE
        MoveHorizontal = Input.GetAxisRaw("Horizontal");
        MoveVertical = Input.GetAxisRaw("Vertical");
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
        {
            anim.SetBool("isMoving", false);
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
            AudioManager.instance.AudioAccess(4);
        }

        if (other.tag == "Whirlpool")
        {
            moveSpeed = slowSpeed;
        }
        //if(other.tag == "SharkSeekPowerUp")
        //{
        //    hasSharkSeekPowerUp = true;
        //    other.gameObject.SetActive(false);
        //    Instantiate(pickUpFX2, myRB.position, myRB.rotation);
        //}
    }

    void OnTriggerExit(Collider other)
    {
        moveSpeed = regularSpeed;
    }

}
