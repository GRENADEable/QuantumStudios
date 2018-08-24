using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashScreen : MonoBehaviour
{
    #region Public Variables
    public float time = 0;
    #endregion

    #region Callbacks
    void Update()
    {
        time += Time.deltaTime;

        if (time >= 6.0f)
        {
            AudioManager.instance.StopSplashScreenAudio();
            SceneManagement.instance.MainMenu();
            AudioManager.instance.AudioAccess(9);
        }
    }
    #endregion
}
