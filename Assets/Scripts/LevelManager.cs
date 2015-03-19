using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour 
{
	public GameObject[] terrainBlocks;
	public int numBlocks = 0;

	private GameObject currentBlock;
	private GameObject nextBlock;
	public GameObject spawnPoint = null;
	private int blockCounter = 1;

	// Use this for initialization
	void Start () 
	{

		currentBlock = Instantiate (terrainBlocks [0], new Vector3(0.0f, 0.0f, 0.0f), new Quaternion(0, 0, 0, 0)) as GameObject;
		nextBlock = Instantiate (terrainBlocks [1], spawnPoint.transform.position, new Quaternion(0, 0, 0, 0)) as GameObject;

		currentBlock.transform.SetParent (transform);
		nextBlock.transform.SetParent (transform);
		blockCounter = 1;
	}
	
	// Update is called once per frame
	void Update () 
	{
		GameObject blockToCheck;
		if (blockCounter % 2 == 0) 
		{
			blockToCheck = currentBlock;
		} 
		else 
		{
			blockToCheck = nextBlock;
		}

		if (blockToCheck.transform.position.x <= 0.0f) 
		{
			blockCounter++;
			int visibletile = blockCounter % 2;

			if(visibletile == 0)
			{

				GameObject toDestroy = currentBlock;
				currentBlock = Instantiate (terrainBlocks [blockCounter % numBlocks], spawnPoint.transform.position, new Quaternion(0, 0, 0, 0)) as GameObject;
				currentBlock.transform.SetParent(transform);
				Vector3 pos = spawnPoint.transform.position;
				pos.x -= .2f;
				currentBlock.transform.position = pos;

				Destroy(toDestroy);
			}
			else
			{
				GameObject toDestroy = nextBlock;
				nextBlock = Instantiate (terrainBlocks [blockCounter % numBlocks], spawnPoint.transform.position, new Quaternion(0, 0, 0, 0)) as GameObject;
				nextBlock.transform.SetParent(transform);
				Vector3 pos = spawnPoint.transform.position;
				pos.x -= .2f;
				nextBlock.transform.position = pos;
				Destroy(toDestroy);
			}

		}


	}
}
