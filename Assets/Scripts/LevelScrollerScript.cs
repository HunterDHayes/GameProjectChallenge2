using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelScrollerScript : MonoBehaviour {

	public float scrollSpeed;
	
	public List<GameObject> floors;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		for(int i = 0; i < transform.childCount; i++)
		{
			transform.GetChild(i).position -= new Vector3(scrollSpeed * Time.deltaTime, 0.0f, 0.0f);
		}
		
		//Check if need to spawn new floor
		
		//Check if need to despawn a floor
	}
}
