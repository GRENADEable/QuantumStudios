using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManage : MonoBehaviour
{
    public GameObject MainMenuObject;
    public GameObject ShopObject;
    /*public GameObject text1;
    public GameObject text2;
    public GameObject text3;
    public GameObject text4;
    public GameObject text5;
    public GameObject text6;
    public GameObject button1;
    public GameObject button2;
    public GameObject button3;

    public GameObject button4;

    public GameObject button5;*/

    // Use this for initialization
    void Awake()
    {
        MainMenuObject.SetActive(true);
        ShopObject.SetActive(false);
        //DontDestroyOnLoad(this);
        /*text2.SetActive(false);
        text3.SetActive(false);
        text4.SetActive(false);
        text5.SetActive(false);
        text6.SetActive(false);*/
    }

    public void PlayMltiPlayer()
    {
        SceneManager.LoadScene("LobbyScene");
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

    /*public void Next()

    {
        text2.SetActive(true);
        text1.SetActive(false);
        button1.SetActive(false);
        button2.SetActive(true);
    }

    public void Next2()
    {
        text3.SetActive(true);
        text2.SetActive(false);
        button2.SetActive(false);
        button3.SetActive(true);
    }

    public void Next3()
    {
        text4.SetActive(true);
        text3.SetActive(false);
        button3.SetActive(false);
        button4.SetActive(true);
    }

    public void Next4()
    {
        text5.SetActive(true);
        text4.SetActive(false);
        button3.SetActive(false);
        button4.SetActive(true);
        SceneManager.LoadScene("ShariqScene");
    }

    public void Next5()
     {
       text6.SetActive(true);
        text5.SetActive(false);
         button4.SetActive(false);
         button5.SetActive(true); 
         SceneManager.LoadScene("ShariqScene");
         
     }*/


}

