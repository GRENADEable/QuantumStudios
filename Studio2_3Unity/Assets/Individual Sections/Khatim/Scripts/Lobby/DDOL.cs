using UnityEngine;

public class DDOL : MonoBehaviour
{
    #region Callbacks
    void Awake()
    {
        DontDestroyOnLoad(this);    
    }
    #endregion
}
