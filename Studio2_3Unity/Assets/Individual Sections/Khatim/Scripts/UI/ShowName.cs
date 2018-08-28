using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowName : Photon.MonoBehaviour
{
    #region Public Variables
    public PlayerNameObj plyNames;
    public Text userName;
    #endregion

    #region Private Variables
    private PhotonView pview;
    #endregion

    #region Unity Callbacks
    void Start()
    {
        pview = GetComponent<PhotonView>();
        Sync();
    }

    /*void Awake()
    {
        //userName = GameObject.FindGameObjectWithTag("PlayerText").GetComponent<Text>();
        userName = GetComponentInChildren<Text>(); //Drag and Drop.
    }*/

    /*void Update()
    {
        userName.text = GetComponent<PhotonView>().owner.NickName;
    }*/
    #endregion

    #region My Functions
    [PunRPC]
    public void Sync()
    {
        pview.RPC("DisplayPlayer", PhotonTargets.AllBuffered, new object[] { plyNames.uesrName });
    }

    [PunRPC]
    public void DisplayPlayer(string user)
    {
        userName.text = user;
    }
    #endregion

    #region PhotonCallbacks
    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(userName);
        }
        else if (stream.isReading)
        {
            userName.text = (string)stream.ReceiveNext();
        }
    }
    #endregion
}
