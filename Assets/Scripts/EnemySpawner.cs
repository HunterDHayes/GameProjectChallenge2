using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour {
	public GameObject Player = null;
	public List<GameObject> enemiesToSpawn;
	public Camera mainCamera = null;
	
	public float minSpawnTime;
	public float maxSpawnTime;
	private float currSpawnTime = 0.0f;
	
	public float spawnLead = 0.0f;

	// Use this for initialization
	void Start () 
	{
		currSpawnTime = 0.0f;
	}
	
	// Update is called once per frame
	void Update () 
	{
		currSpawnTime -= Time.deltaTime;
		if (currSpawnTime <= 0.0f) 
		{
			GameObject NewObject = null;
		
			//actual instantiation goes here
			int num = Random.Range(0, 30);
			
			if(num < 10){
				NewObject = GameObject.Instantiate(enemiesToSpawn[1]);
				NewObject.transform.position = new Vector3(mainCamera.transform.position.x + spawnLead,Random.Range(3.0f, 5.0f));
			}
			else if(num < 20) {
				NewObject = GameObject.Instantiate(enemiesToSpawn[2]);
				NewObject.transform.position = new Vector3(mainCamera.transform.position.x + spawnLead,0.0f);
			}
			else {
				NewObject = GameObject.Instantiate(enemiesToSpawn[0]);
				NewObject.transform.position = new Vector3(mainCamera.transform.position.x + spawnLead,0.0f);
			}
			
			NewObject.transform.SetParent(transform);

			currSpawnTime = Random.Range(minSpawnTime, maxSpawnTime);
		}
	}
}
