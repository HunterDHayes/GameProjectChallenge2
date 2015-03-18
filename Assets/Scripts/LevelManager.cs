using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour 
{
	public GameObject[] terrainBlocks;
	public int numBlocks = 0;

	private GameObject currentBlock;
	private GameObject nextBlock;
	public float xOffsetPerTile = 0.0f;
	public float yOffsetPerTile = 0.0f;
	private int blockCounter = 1;

	// Use this for initialization
	void Start () 
	{

		currentBlock = Instantiate (terrainBlocks [0], new Vector3(0.0f, yOffsetPerTile, 0.0f), new Quaternion(0, 0, 0, 0)) as GameObject;
		nextBlock = Instantiate (terrainBlocks [1], new Vector3(terrainBlocks [1].transform.localScale.x, yOffsetPerTile, 0.0f), new Quaternion(0, 0, 0, 0)) as GameObject;

		currentBlock.transform.SetParent (transform);
		nextBlock.transform.SetParent (transform);
		blockCounter = 1;
	}
	
	// Update is called once per frame
	void Update () 
	{
		int tilenum = blockCounter % 2;

		GameObject blockToCheck;
		if (tilenum == 0) 
		{
			blockToCheck = currentBlock;
		} 
		else 
		{
			blockToCheck = nextBlock;
		}
	

		if (blockToCheck.transform.position.x <= -(blockToCheck.transform.localScale.x)) 
		{
			blockCounter++;
			int visibletile = blockCounter % 2;

			if(visibletile == 0)
			{
				Destroy(currentBlock);
				currentBlock = Instantiate (terrainBlocks [blockCounter % numBlocks], new Vector3(terrainBlocks [blockCounter % numBlocks].transform.localScale.x, yOffsetPerTile, 0.0f), new Quaternion(0, 0, 0, 0)) as GameObject;
				currentBlock.transform.SetParent(transform);
			}
			else
			{
				Destroy(nextBlock);
				nextBlock = Instantiate (terrainBlocks [blockCounter % numBlocks], new Vector3(terrainBlocks [blockCounter % numBlocks].transform.localScale.x, yOffsetPerTile, 0.0f), new Quaternion(0, 0, 0, 0)) as GameObject;
				nextBlock.transform.SetParent(transform);
			}

		}
	}
}
