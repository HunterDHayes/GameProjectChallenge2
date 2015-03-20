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
	public int NumOfSpawns = 0;
	
	public float spawnLead = 0.0f;

	// Use this for initialization
	void Start () 
	{
		currSpawnTime = 0.0f;
	}
	
	// Update is called once per frame
	void Update () 
	{	
		currSpawnTime -= Time.deltaTime * (GetComponent<LevelScrollerScript>().scrollSpeed / 5.0f);
		if (currSpawnTime <= 0.0f) 
		{
			GameObject NewObject = null;
		
			//actual instantiation goes here
			int num = Random.Range(0,NumOfSpawns);

			if(num == 0) {
				NewObject = GameObject.Instantiate(enemiesToSpawn[4]);
				NewObject.transform.position = new Vector3(mainCamera.transform.position.x + spawnLead,3.0f);
			} else if(num == 1) {
				NewObject = GameObject.Instantiate(enemiesToSpawn[5]);
				NewObject.transform.position = new Vector3(mainCamera.transform.position.x + spawnLead,3.0f);
			} else if(num == 2){
				NewObject = GameObject.Instantiate(enemiesToSpawn[1]);
				NewObject.transform.position = new Vector3(mainCamera.transform.position.x + spawnLead,Random.Range(3.0f, 5.0f));
			}
			else if(num == 3) {
				NewObject = GameObject.Instantiate(enemiesToSpawn[2]);
				NewObject.transform.position = new Vector3(mainCamera.transform.position.x + spawnLead,3.0f);
			}
			else if(num == 4) {
				NewObject = GameObject.Instantiate(enemiesToSpawn[3]);
				NewObject.transform.position = new Vector3(mainCamera.transform.position.x + spawnLead,3.0f);
			}
			else if(num == 5){
				NewObject = GameObject.Instantiate(enemiesToSpawn[0]);
				NewObject.transform.position = new Vector3(mainCamera.transform.position.x + spawnLead,3.0f);
			}
			else
			{
				NewObject = GameObject.Instantiate(enemiesToSpawn[6]);
				NewObject.transform.position = new Vector3(mainCamera.transform.position.x + spawnLead,3.0f);
			}
			
			NewObject.transform.SetParent(transform);

			currSpawnTime = Random.Range(minSpawnTime, maxSpawnTime);
		}
	}
}
