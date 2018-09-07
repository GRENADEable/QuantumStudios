using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhirlpoolActivate : Photon.MonoBehaviour
{
    #region Public Variables
    public static WhirlpoolActivate instance;
    public float maxSize;
    public float minSize;
    public float timer;
    public float maxTime;
    public int isActivated;
    #endregion

    #region Private Variables
    [SerializeField]
    private float growthSize;
    /*[SerializeField]
    private bool isActivated;*/
    //private PhotonView pview;
    [SerializeField]
    private PlayerController player;
    //[SerializeField]
    //private int whirlPoolID;
    /*[SerializeField]
    private float movementValue = 0.25f;
    [SerializeField]
    private float rotateValue = 500f;
    private Vector3 tarPos;
    private Quaternion tarRot;*/
    #endregion

    #region Unity Callbacks
    void Start()
    {
        //pview = GetComponent<PhotonView>();
        //player = GameObject.FindObjectOfType<PlayerController>();
        //whirlPoolID = 0;
        //isActivated = 0;
        base.photonView.TransferOwnership(6);
    }
    void Update()
    {
        /*if (isActivated)
        {
            Debug.Log(growthSize);
            transform.localScale = new Vector3(Mathf.Lerp(minSize, maxSize, growthSize), Mathf.Lerp(minSize, maxSize, growthSize), 0.06062245f);
            growthSize += 0.1f;
        }
        else
        {
            transform.localScale = new Vector3(Mathf.Lerp(minSize, maxSize, growthSize), Mathf.Lerp(minSize, maxSize, growthSize), 0.06062245f);
            growthSize -= 0.1f;
        }*/
        growthSize = Mathf.Clamp01(growthSize);
        if (growthSize >= 0.1f)
        {
            timer -= Time.deltaTime;
            if (timer <= 0 && isActivated == 1)
            {
                timer = maxTime;
                isActivated = 0;
                base.photonView.RPC("DeactivateWhirlpool", PhotonTargets.All, 0);
                //base.photonView.TransferOwnership(6);
                //Debug.LogWarning("Ownership Transferred to Scene");
                //isActivated = false;
            }
        }

        else if (growthSize <= 0f)
        {
            timer -= Time.deltaTime;
        }
    }

    void OnTriggerStay(Collider other)
    {
        /*if (base.photonView.owner != PhotonNetwork.player && !isActivated && timer <= 0)
        {
            base.photonView.RequestOwnership();
            Debug.LogWarning("Ownership Transferred to Player");
        }*/

        if (Input.GetKey(KeyCode.E) && timer <= 0 && isActivated == 0)
        {
            AudioManager.instance.AudioAccess(8);
            isActivated = 1;
            base.photonView.RPC("ActivateWhirlpool", PhotonTargets.All, 1);
            timer = maxTime;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        /*if (base.photonView.owner != player.photonView.owner && isActivated)
        {
            other.gameObject.SetActive(false);
            GameManager.instance.index = Random.Range(0, GameManager.instance.spawnLocation.Length);
            other.gameObject.transform.position = GameManager.instance.spawnLocation[GameManager.instance.index].transform.position;
            other.gameObject.SetActive(true);
            Debug.LogWarning("Player Dead");
        }*/
        player = GameObject.FindObjectOfType<PlayerController>();

        if (base.photonView.owner != player.photonView.owner && isActivated == 1)
        {
            /*other.gameObject.SetActive(false);
            GameManager.instance.index = Random.Range(0, GameManager.instance.spawnLocation.Length);
            other.gameObject.transform.position = GameManager.instance.spawnLocation[GameManager.instance.index].transform.position;
            other.gameObject.SetActive(true);*/
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
        isActivated = 1;
        transform.localScale = new Vector3(maxSize, maxSize, 0.04f);
        growthSize += 0.1f;
        base.photonView.RequestOwnership();
    }

    [PunRPC]
    public void DeactivateWhirlpool(int isActivated)
    {
        isActivated = 0;
        transform.localScale = new Vector3(1f, 1f, 0.04f);
        growthSize -= 0.1f;
        base.photonView.TransferOwnership(6);
    }

    /*void SmoothMovement()
    {
        transform.position = Vector3.Lerp(transform.position, tarPos, movementValue);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, tarRot, rotateValue * Time.deltaTime);
    }*/
    #endregion

    /*#region Photon Callbacks
    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(transform.position);
            stream.SendNext(transform.localScale);

        }
        else
        {
            this.transform.position = (Vector3)stream.ReceiveNext();
            this.transform.localScale = (Vector3)stream.ReceiveNext();
        }
    }
    #endregion*/
}
