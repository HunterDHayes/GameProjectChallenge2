using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {
	public GameObject Player = null;
	public GameObject EnemyTypeToSpawn = null;
	public Camera mainCamera = null;
	public float spawnLead = 0.0f;
	public float spawnRangeMin = 0.0f;
	public float spawnRangeMax = 0.0f;
	public float spawnDelay = 0.0f;
	public float spawnRateMin = 0.0f;
	public float spawnRateMax = 0.0f;


	public float offsetBetweenSpawns;

	private float currSpawnTime;

	// Use this for initialization
	void Start () 
	{
		currSpawnTime = 0.0f;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Player != null) 
		{
			currSpawnTime += Time.deltaTime;
			if (currSpawnTime >= spawnDelay) 
			{
				//spawn the enemies

				float baseSpawnPoint = spawnLead; //+ Random.Range (spawnRangeMin, spawnRangeMax

				float spawnrate = Random.Range (spawnRateMin, spawnRateMax);
				for (int i = 0; i < spawnrate; i++) 
				{
					//spawn the enemies at this point
					baseSpawnPoint += (offsetBetweenSpawns * i);
					//actual instantiation goes here
					GameObject NewObject = GameObject.Instantiate(EnemyTypeToSpawn);
					Vector3 pos = new Vector3(baseSpawnPoint,0.0f);
					NewObject.transform.position =  pos;

					RaycastHit2D ray = Physics2D.Raycast(NewObject.transform.position,-transform.up, 100);
					pos.y -=  ray.distance;

					NewObject.transform.SetParent(transform);
					
				}

				currSpawnTime = 0.0f;
			}
		}


	}
}
