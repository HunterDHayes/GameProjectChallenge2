using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {
	public GameObject Player = null;
	public GameObject EnemyTypeToSpawn = null;
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
			//actual instantiation goes here
			GameObject NewObject = GameObject.Instantiate(EnemyTypeToSpawn);
			Vector3 pos = new Vector3(mainCamera.transform.position.x + spawnLead,0.0f);
			NewObject.transform.position = pos;

			NewObject.transform.SetParent(transform);

			currSpawnTime = Random.Range(minSpawnTime, maxSpawnTime);
		}
	}
}
