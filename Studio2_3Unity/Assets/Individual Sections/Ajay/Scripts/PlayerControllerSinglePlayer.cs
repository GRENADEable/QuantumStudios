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
    public float spDuration;
    #endregion

    #region Private Variables
    private Rigidbody myRB;
    private Vector3 movementInput;
    [SerializeField]
    private float timer;
    [Header("Mobile Joystick")]
    [SerializeField]
    private MobileJoystick mobileJoy;
    private GameObject mobilePrefab;
    [SerializeField]
    private Animator anim;
    private float MoveHorizontal;
    private float MoveVertical;
    private int index;
    [Header("Shark Spawning")]

    private SharkSpawning spawner;
    #endregion

    void Start()
    {
        myRB = GetComponent<Rigidbody>();
        mobilePrefab = GameObject.FindGameObjectWithTag("Joystick");
        mobileJoy = GameObject.FindGameObjectWithTag("Joystick").GetComponent<MobileJoystick>();
        anim = GetComponent<Animator>();

        //sharkSpawnLocation = GameObject.FindGameObjectsWithTag("SharkSpawner");
        spawner = GameObject.FindGameObjectWithTag("Spawner").GetComponent<SharkSpawning>();
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

        /*if (sharkCount <= 3 && Input.GetKeyDown(KeyCode.F)) //Testing
        {
            index = Random.Range(0, sharkSpawnLocation.Length);

            Instantiate(shark, sharkSpawnLocation[index].transform.position, Quaternion.identity);

            sharkCount++;
        }*/
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
            spawner.ActivateEnemy();
            Debug.LogWarning("Hit Whirlpool, Spawning Shark for PC Build");
        }
    }

    void OnTriggerExit(Collider other)
    {
        moveSpeed = regularSpeed;
    }

}
