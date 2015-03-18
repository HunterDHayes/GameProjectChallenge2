using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {
	public GameObject Player;
	public Camera mainCamera;
	public float spawnRangeMin;
	public float spawnRangeMax;
	public float spawnDelay;
	public float spawnRateMin;
	public float spawnRateMax;


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
		currSpawnTime += Time.deltaTime;
		if (currSpawnTime >= spawnDelay)
		{
			//spawn the enemies
			float spawnArea = Player.transform.position.x + Screen.width /2;
			float baseSpawnPoint = spawnArea + Random.Range(spawnRangeMin, spawnRangeMax);

			float spawnrate = Random.Range(spawnRateMin,spawnRateMax);
			for (int i = 0; i < spawnrate; i++) 
			{
				//spawn the enemies at this point
				baseSpawnPoint += (offsetBetweenSpawns * i);

				//actual instantiation goes here

			}

		}



	}
}
