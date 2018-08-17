using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour {

	public float maxTime;
	public float timer  = 0;
	
	[SerializeField]
	private GameObject[] powerupSpawnLocation;
	[SerializeField]
	private int index;
	[SerializeField]
	private GameObject powerUp;
	[SerializeField]
	private List<GameObject> powerPickUp;
	
	void Awake()
	{
		timer = maxTime;
		powerupSpawnLocation = GameObject.FindGameObjectsWithTag("SpeedPowerSpawn");
		for (int i = 0; i < powerupSpawnLocation.Length; i++)
		{
			GameObject obj = Instantiate(powerUp, powerupSpawnLocation[i].transform.position, Quaternion.identity);
			powerUp.SetActive(false);
			powerPickUp.Add(obj);
		}
	}
	void Start () 
	{	

	}
	
	
	void FixedUpdate () 
	{
		index = Random.Range(0, powerPickUp.Count);
		timer -= Time.deltaTime;
		if(timer <= 0)
		{
			Spawner();
			timer = maxTime;
		}
	}
	void Spawner()
	{
		if (!powerPickUp[index].activeInHierarchy)
		{
			powerPickUp[index].SetActive(true);
		}
	}
}
