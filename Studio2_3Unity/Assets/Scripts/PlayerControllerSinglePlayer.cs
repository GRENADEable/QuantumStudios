using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControllerSinglePlayer : MonoBehaviour
{
    #region Public Variables
    [Header("Movement Speed")]
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
    [SerializeField] private GameObject mobilePrefab;
    private float MoveHorizontal;
    private float MoveVertical;
    [Header("Other")]
    [SerializeField] private float spDuration = default;
    [SerializeField] private float timer;
    [SerializeField] private Animator anim;
    #endregion

    void Start()
    {
        myRB = GetComponent<Rigidbody>();
        mobilePrefab = GameObject.FindGameObjectWithTag("Joystick");
        anim = GetComponent<Animator>();
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
        }
#if UNITY_EDITOR || UNITY_STANDALONE
        if (other.tag == "Whirlpool")
        {
            moveSpeed = slowSpeed;
            Debug.LogWarning("Hit Whirlpool, Spawning Shark for PC Build");
        }
#else
        if (other.tag == "Whirlpool")
        {
            moveSpeed = slowSpeed;
            Debug.LogWarning("Hit Whirlpool, Spawning Shark for Android Build");
        }
#endif
    }

    void OnTriggerExit(Collider other)
    {
        moveSpeed = regularSpeed;
    }

}
