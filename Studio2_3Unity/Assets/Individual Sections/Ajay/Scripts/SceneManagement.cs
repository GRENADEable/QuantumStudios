using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    public static SceneManagement instance = null;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
    public void SinglePlayer()
    {
        SceneManager.LoadScene("SinglePlayer");
        AudioManager.instance.StopAudio();
        AudioManager.instance.PlaySinglePlayerAudio();

    }

    public void Quit()
    {
        Application.Quit();
        Debug.LogWarning("Quit");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        AudioManager.instance.StopAudio();
        AudioManager.instance.MainMenuMusic();
    }

    public void GameOver()
    {
        SceneManager.LoadScene("GameOverScene");
        AudioManager.instance.StopAudio();
        AudioManager.instance.DeathAudio();
    }
}
