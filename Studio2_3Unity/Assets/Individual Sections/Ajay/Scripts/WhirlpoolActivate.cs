using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhirlpoolActivate : Photon.MonoBehaviour
{
    #region Public Variables
    [Header("Wirlpool Speed")]
    public float clampMax;
    [Header("Whirlpool Sizes")]
    public float maxSize;
    public float minSize;
    [Header("Timers")]
    public float timer;
    public float maxTime;
    [Header("Whirlpool Other")]
    public int isActivated;
    public bool activation;
    #endregion

    #region Private Variables
    [SerializeField]
    private float growthSize;
    [SerializeField]
    private PlayerController player;
    private Vector3 movementInput;
    private Rigidbody rg;
    #endregion

    #region Unity Callbacks
    void Start()
    {
        //base.photonView.TransferOwnership(6);
        rg = GetComponent<Rigidbody>();
    }
    void Update()
    {
        growthSize = Mathf.Clamp01(growthSize);
        if (growthSize >= 0.1f)
        {
            timer -= Time.deltaTime;
            if (timer <= 0 && isActivated == 1)
            {
                timer = maxTime;
                isActivated = 0;
                activation = true;
                base.photonView.TransferOwnership(0);
                base.photonView.RPC("DeactivateWhirlpool", PhotonTargets.AllBufferedViaServer, isActivated);
            }
        }

        else if (growthSize <= 0f)
        {
            timer -= Time.deltaTime;
        }
    }

    /*void FixedUpdate()
    {
        if (base.photonView.owner == PhotonNetwork.player)
            MoveWhirlpool();
        else
            rg.velocity = Vector3.zero;
    }*/

    void OnTriggerStay(Collider other)
    {
        if (Input.GetKey(KeyCode.E) && timer <= 0 && isActivated == 0)
        {
            AudioManager.instance.AudioAccess(8);
            isActivated = 1;
            activation = true;
            timer = maxTime;
            base.photonView.TransferOwnership(PhotonNetwork.player);
            base.photonView.RPC("ActivateWhirlpool", PhotonTargets.AllBufferedViaServer, isActivated);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        player = GameObject.FindObjectOfType<PlayerController>();

        if (base.photonView.owner != PhotonNetwork.player && (activation || isActivated == 1) && timer >= 0)
        {
            other.gameObject.transform.position = GameManager.instance.playerSpawnLocation[GameManager.instance.index].transform.position;
            AudioManager.instance.AudioAccess(5);
            Debug.LogWarning("Player Dead");
        }
    }

    void OnTriggerExit(Collider other)
    {
        player = null;
    }
    #endregion

    #region My Functions
    [PunRPC]
    public void ActivateWhirlpool(int isActivated)
    {
        transform.localScale = new Vector3(maxSize, maxSize, 0.04f);
        growthSize += 0.1f;
        activation = true;
        timer = maxTime;
    }

    [PunRPC]
    public void DeactivateWhirlpool(int isActivated)
    {
        transform.localScale = new Vector3(1f, 1f, 0.04f);
        growthSize -= 0.1f;
        activation = false;
        timer = maxTime;
    }

    /*void MoveWhirlpool()
    {
#if UNITY_EDITOR || UNITY_STANDALONE
        float MoveHorizontal = Input.GetAxisRaw("Horizontal");
        float MoveVertical = Input.GetAxisRaw("Vertical");
#else
        float MoveHorizontal = PlayerController.instance.mobileJoy.Horizontal();
        float MoveVertical = PlayerController.instance.mobileJoy.Vertical();
#endif

        movementInput = new Vector3(MoveHorizontal, 0.0f, MoveVertical);
        movementInput = Vector3.ClampMagnitude(movementInput, clampMax);
        rg.AddForce(movementInput, ForceMode.Impulse);
    }*/
    #endregion

    /*#region Photon Callbacks
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
            stream.SendNext(activation);
        else
            activation = (bool)stream.ReceiveNext();
    }
    #endregion*/
}
