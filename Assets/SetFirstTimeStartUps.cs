﻿using UnityEngine;
using System.Collections;

public class SetFirstTimeStartUps : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
		if (PlayerPrefs.GetInt ("FirstTimeStartUP") == 0) {
			PlayerPrefs.SetInt ("FirstTimeStartUP", 1);
		} 
	}

}