using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManage : MonoBehaviour {
	public GameObject MainMenuObject;
	public GameObject ShopObject;
	// Use this for initialization
	void Awake () {

		MainMenuObject.SetActive(true);
		ShopObject.SetActive(false);
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void PlaySinglePlayer()
	{
		SceneManager.LoadScene("Level");
	}

	public void Shop()
	{
		MainMenuObject.SetActive(false);
		ShopObject.SetActive(true);
	}

	public void Quit()
	{
		Application.Quit();
	}
}
