using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour {
	public GameObject Player = null;
	public List<GameObject> enemiesToSpawn;
	public List<GameObject> powerUpsToSpawn;
	public Camera mainCamera = null;
	
	public float minSpawnTime;
	public float maxSpawnTime;
	private float currSpawnTime = 0.0f;
	
	public float minPowerSpawn;
	public float maxPowerSpawn;
	private float currPowerSpawnTime = 0.0f;
	
	public float spawnLead = 0.0f;

	// Use this for initialization
	void Start () 
	{
		currSpawnTime = 0.0f;
		Random.seed = (int)Time.realtimeSinceStartup;
	}
	
	// Update is called once per frame
	void Update () 
	{	

		currSpawnTime -= Time.deltaTime * (GetComponent<LevelScrollerScript>().scrollSpeed / 5.0f);
		if (currSpawnTime <= 0.0f) 
		{
			GameObject NewObject = null;
		
			//actual instantiation goes here
			int num = Random.Range(0,enemiesToSpawn.Count);
			
			if(num == 1) {
				NewObject = GameObject.Instantiate(enemiesToSpawn[num]);
				NewObject.transform.position = new Vector3(mainCamera.transform.position.x + spawnLead,Random.Range(3.0f, 5.0f));
			}
			else {
				NewObject = GameObject.Instantiate(enemiesToSpawn[num]);
				NewObject.transform.position = new Vector3(mainCamera.transform.position.x + spawnLead,3.0f);
			}
			
			NewObject.transform.SetParent(transform);

			currSpawnTime = Random.Range(minSpawnTime, maxSpawnTime);
		}
		
		
		currPowerSpawnTime -= Time.deltaTime * (GetComponent<LevelScrollerScript>().scrollSpeed / 5.0f);
		if (currPowerSpawnTime <= 0.0f) 
		{
			GameObject NewObject = null;
			
			//actual instantiation goes here
			int num = Random.Range(0,powerUpsToSpawn.Count);
			
			NewObject = GameObject.Instantiate(powerUpsToSpawn[num]);
			NewObject.transform.position = new Vector3(mainCamera.transform.position.x + spawnLead,3.0f);
			
			NewObject.transform.SetParent(transform);
			
			currPowerSpawnTime = Random.Range(minPowerSpawn, maxPowerSpawn);
		}
	}
}
