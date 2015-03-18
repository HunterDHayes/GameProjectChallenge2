using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		float dX = Input.GetAxis("Horizontal");
		GetComponent<Rigidbody2D>().AddForce(new Vector2(dX * 20, 0.0f), ForceMode2D.Force);
	}
}
