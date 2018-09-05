using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerConrollerTest : MonoBehaviour
{
    #region Public Variables 
    public float moveSpeed;
    public float slowSpeed;
    public float regularSpeed;
    public float powerUpSpeed;
    public float clampMax;

    #endregion

    #region Private Variables
    private Rigidbody myRB;
    private Vector3 movementInput;
    [Header("Mobile Joystick")]
    [SerializeField]
    private MobileJoystick mobileJoy;
    private GameObject mobilePrefab;
    [SerializeField]
    private float spDuration;
    [SerializeField]
    private float timer;
    private Animator anim;
    private float MoveHorizontal;
    private float MoveVertical;
    [SerializeField]
    private GameObject shark;
    #endregion

    #region Unity Callbacks
    void Start()
    {
        myRB = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        mobilePrefab = GameObject.FindGameObjectWithTag("Joystick");
        mobileJoy = GameObject.FindGameObjectWithTag("Joystick").GetComponent<MobileJoystick>();
        shark = GameObject.FindGameObjectWithTag("AIShark");
        shark.SetActive(false);
    }

    void FixedUpdate()
    {

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
        if (other.tag == "SharkCanvas")
        {
            shark.SetActive(true);
        }
    }
    void OnTriggerExit(Collider other)
    {
        moveSpeed = regularSpeed;
    }
    #endregion
}