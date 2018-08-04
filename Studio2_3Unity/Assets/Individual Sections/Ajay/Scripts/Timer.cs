using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

	public float timerCount = 10f;

	private Text timerSecond;
	
	void Start () 
	{
		timerSecond = GetComponent<Text>();
	}
	
	
	void Update () 
	{
		timerCount += Time.deltaTime;
		timerSecond.text = timerCount.ToString ("Time: 0");
	}
}
