using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhirlpoolActivate : MonoBehaviour
{
    #region Public Variables
    public float maxSize;
    public float minSize;
    public float timer;
    #endregion

    #region Private Variables
    [SerializeField]
    private float growthSize;
    /*[SerializeField]
    private bool isActivated;*/
    private PhotonView pview;
    [SerializeField]
    private PlayerController player;
    [SerializeField]
    private int whirlPoolID;
    [SerializeField]
    private int isActivated;
    #endregion

    #region Unity Callbacks
    void Start()
    {
        pview = GetComponent<PhotonView>();
        player = GameObject.FindObjectOfType<PlayerController>();
        whirlPoolID = 0;
        isActivated = 0;
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
                timer = 5.0f;
                //whirlPoolID = 0;
                isActivated = 0;
                pview.RPC("DeactivateWhirlpool", PhotonTargets.All, isActivated = 0);
            }
        }
        else if (growthSize <= 0f)
        {
            timer -= Time.deltaTime;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (Input.GetKey(KeyCode.E) && timer <= 0)
        {
            AudioManager.instance.AudioAccess(8);
            //this.pview.viewID = player.photonView.viewID;
            isActivated = 1;
            pview.RPC("ActivateWhirlpool", PhotonTargets.All, isActivated = 1);
            timer = 5;
        }

        /*if (whirlPoolID != player.playerID && isActivated == 1)
        {
            other.gameObject.SetActive(false);
        }*/
    }
    #endregion

    #region My Functions
    [PunRPC]
    public void ActivateWhirlpool(int isActivated = 1)
    {
        transform.localScale = new Vector3(Mathf.Lerp(minSize, maxSize, growthSize), Mathf.Lerp(minSize, maxSize, growthSize), 0.06062245f);
        growthSize += 0.1f;
    }

    [PunRPC]
    public void DeactivateWhirlpool(int isActivated = 0)
    {
        /*transform.localScale = new Vector3(Mathf.Lerp(minSize, maxSize, growthSize), Mathf.Lerp(minSize, maxSize, growthSize), 0.06062245f);
        growthSize -= 0.1f;*/
        transform.localScale = new Vector3(1f, 1f, 0.04f);
        //isActivated = false;
    }
    #endregion
}
