using UnityEngine;
using System.Collections;

public class CameraFollowScript : MonoBehaviour {

	private GameObject player = null;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if(!player) player = GameObject.FindGameObjectWithTag("Player");
		if(player)
		{
			
		}
	}
}
