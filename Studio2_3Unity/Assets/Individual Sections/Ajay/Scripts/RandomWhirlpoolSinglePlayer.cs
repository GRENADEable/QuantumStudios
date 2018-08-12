using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomWhirlpoolSinglePlayer : MonoBehaviour 
{
	public float maxSize;
	public float growInt;
	public float waitTime;
	
	void Start () 
	{
		StartCoroutine(Scale());
	}

	IEnumerator Scale()
	{
		float timer = 0;

		while(true)
		{
			//Scale all the axis to have the same value to work with float instead of comparing via vectors
			while(maxSize > transform.localScale.x)
			{
				timer += Time.deltaTime;
				transform.localScale += new Vector3(5f,0.37105f,3.69958f) * Time.deltaTime * growInt * Random.Range(1f, 10f);
				yield return null;
			}
			
			//Reset the timer
			yield return new WaitForSeconds(waitTime);

			timer = 0;
			while(1 < transform.localScale.x)
			{
				timer += Time.deltaTime;
				transform.localScale -= new Vector3(5f,0.37105f,3.69958f) * Time.deltaTime * growInt * Random.Range(1f, 10f);
				yield return null;
			}

			timer = 0;
			yield return new WaitForSeconds(waitTime);
		}
	}
	void Update()
	{
		
	}
	
}
