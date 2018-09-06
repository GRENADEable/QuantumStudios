using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashScreen : MonoBehaviour
{
    #region Public Variables
    public float time = 0;
    #endregion

    #region Callbacks
    void Start()
    {
        AudioManager.instance.SplashScreenAudio();
    }
    void Update()
    {
        time += Time.deltaTime;

        if (time >= 5.3f)
        {
            SceneManagement.instance.TutorialScene();
        }
    }
    #endregion
}
