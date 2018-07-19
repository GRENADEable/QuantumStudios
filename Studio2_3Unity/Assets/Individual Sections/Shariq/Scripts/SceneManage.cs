using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManage : MonoBehaviour
{
    public GameObject MainMenuObject;
    public GameObject ShopObject;
    // Use this for initialization
    void Awake()
    {
        MainMenuObject.SetActive(true);
        ShopObject.SetActive(false);
        DontDestroyOnLoad(this);
    }

    public void PlaySinglePlayer()
    {
        SceneManager.LoadScene("IntegrateScene");
    }

    public void Shop()
    {
        MainMenuObject.SetActive(false);
        ShopObject.SetActive(true);
    }

    public void MainMenu()
    {
        ShopObject.SetActive(false);
        MainMenuObject.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
    
    }

    public void Back()
    {
        SceneManager.LoadScene("LoginTestScene");
    }
}
