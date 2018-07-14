using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    #region Public Variables
    public static CanvasManager instance;
    #endregion

    #region  Private Variables
    [SerializeField]
    private CanvasLobby _canvasLobby;
    public CanvasLobby canvasLobby
    {
        get
        {
            return _canvasLobby;
        }
    }
    #endregion

    #region Unity Callbacks
    void Awake()
    {
        instance = this;
    }
    #endregion
}
