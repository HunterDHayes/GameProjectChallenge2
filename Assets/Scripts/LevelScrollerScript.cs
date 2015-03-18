﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelScrollerScript : MonoBehaviour {

	public float scrollSpeed;

	public List<GameObject> floors;

	public float timer = 0.0f;
	public float speedUpTime = 30.0f;
	public float maxScrollSpeed = 15.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		this.timer += Time.deltaTime;

		if (this.timer >= speedUpTime) {
			timer = 0.0f;
			scrollSpeed *= 1.05f;
			scrollSpeed = Mathf.Min(scrollSpeed, this.maxScrollSpeed);
		}

		for(int i = 0; i < transform.childCount; i++)
		{
			transform.GetChild(i).position -= new Vector3(scrollSpeed * Time.deltaTime, 0.0f, 0.0f);
		}



		//Check if need to spawn new floor
		
		//Check if need to despawn a floor
	}
}
