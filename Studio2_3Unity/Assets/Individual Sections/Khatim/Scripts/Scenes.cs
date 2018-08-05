using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scenes : MonoBehaviour
{
    #region My Functions
    public void MainMenu()
    {
        SceneManager.LoadScene("LobbyScene");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
    #endregion
}
