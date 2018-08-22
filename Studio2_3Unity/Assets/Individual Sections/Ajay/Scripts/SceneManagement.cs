using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour {

	public static SceneManagement instance = null;
	void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
		else if (instance != this)
		{
			Destroy (gameObject);
		}
		DontDestroyOnLoad (gameObject);
	}
	public void SinglePlayer () 
	{
		SceneManager.LoadScene("AjayScene");
	}
	
	public void Quit()
	{
		Application.Quit();
		Debug.Log("Quit");
	}

	public void MainMenu()
	{
		SceneManager.LoadScene("LobbyScene");
	}
	
	void Update () 
	{
		
	}
}
