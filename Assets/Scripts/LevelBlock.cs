using UnityEngine;
using System.Collections;

public class LevelBlock : MonoBehaviour {

	public float speed = 15.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 position = transform.position;
		position.x -= Time.deltaTime * speed;
		transform.position = position;
	}
}
