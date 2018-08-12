using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

	public Text scoreText;
	public float scoreCount;
	public float pointsPerSec;

	
	void Start () 
	{
		
	}
	
	
	void Update () 
	{
		
		scoreCount += pointsPerSec * Time.deltaTime;
		scoreText.text = "Score: " + Mathf.Round (scoreCount);
	}
}
