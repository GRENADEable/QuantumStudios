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
    [SerializeField]
    private bool isActivated;
    private PhotonView pview;
    [SerializeField]
    private PlayerController player;
    [SerializeField]
    private int whirlPoolID;
    #endregion

    #region Unity Callbacks
    void Start()
    {
        pview = GetComponent<PhotonView>();
        player = GameObject.FindObjectOfType<PlayerController>();
        whirlPoolID = 0;
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
            if (timer <= 0 && isActivated)
            {

                timer = 5.0f;
                pview.RPC("DeactivateWhirlpool", PhotonTargets.All, isActivated = false);
                whirlPoolID = 0;
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
        if (Input.GetKey(KeyCode.E) && timer <= 0)
        {
            AudioManager.instance.AudioAccess(8);
            //isActivated = true;
            pview.RPC("ActivateWhirlpool", PhotonTargets.All, isActivated = true);
            this.whirlPoolID = player.playerID;
            timer = 5;
        }
    }
    #endregion

    #region My Functions
    [PunRPC]
    public void ActivateWhirlpool(bool isActivated = true)
    {
        transform.localScale = new Vector3(Mathf.Lerp(minSize, maxSize, growthSize), Mathf.Lerp(minSize, maxSize, growthSize), 0.06062245f);
        growthSize += 0.1f;
    }

    [PunRPC]
    public void DeactivateWhirlpool(bool isActivated = false)
    {
        /*transform.localScale = new Vector3(Mathf.Lerp(minSize, maxSize, growthSize), Mathf.Lerp(minSize, maxSize, growthSize), 0.06062245f);
        growthSize -= 0.1f;*/
        transform.localScale = new Vector3(1f, 1f, 0.04f);
        //isActivated = false;
    }
    #endregion
}
