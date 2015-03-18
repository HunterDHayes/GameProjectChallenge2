using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour 
{
	public GameObject[] terrainBlocks;

	private GameObject currentBlock;
	private GameObject nextBlock;

	private int blockCounter = 0;

	// Use this for initialization
	void Start () 
	{
		this.currentBlock = Instantiate (terrainBlocks [0], new Vector3(0.0f, -7.5f, 0.0f), new Quaternion(0, 0, 0, 0)) as GameObject;
		this.nextBlock = Instantiate (terrainBlocks [1], new Vector3(50.0f, -7.5f, 0.0f), new Quaternion(0, 0, 0, 0)) as GameObject;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (this.currentBlock.transform.position.x < -50.0f) 
		{
			Destroy (this.currentBlock);

			this.currentBlock = this.nextBlock;

			this.nextBlock = Instantiate (terrainBlocks [blockCounter++], new Vector3(this.currentBlock.transform.position.x + 50.0f, -7.5f, 0.0f), new Quaternion(0, 0, 0, 0)) as GameObject;

			this.blockCounter = this.blockCounter % 2;
		}
	}
}
