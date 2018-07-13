using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNetwork : MonoBehaviour
{
    #region Public Variables
    public static PlayerNetwork instance;
    public string userName { get; private set; }
    #endregion

    #region  Private Variables

    #endregion

    #region Callbacks
    void Awake()
    {
        instance = this;

        userName = "Khatim#" + Random.Range(1000, 9999);
        //e.g: Khatim#1952;
    }
    #endregion
}
