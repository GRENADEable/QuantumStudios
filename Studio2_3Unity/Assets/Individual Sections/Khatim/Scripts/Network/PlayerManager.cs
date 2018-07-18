using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : Photon.PunBehaviour
{
    #region  Public Variables
    public static GameObject localPlyInstance;
    #endregion

    #region  Private Variables

    #endregion

    #region Unity Callbacks
    void Awake()
    {
        if (photonView.isMine)
        {
            PlayerManager.localPlyInstance = this.gameObject;
        }

        DontDestroyOnLoad(this.gameObject);
    }
    #endregion

    #region My Functions

    #endregion

    #region Photon Callbacks

    #endregion
}
