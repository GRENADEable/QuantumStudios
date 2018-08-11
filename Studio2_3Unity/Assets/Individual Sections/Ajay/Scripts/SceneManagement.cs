using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour {

	
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
