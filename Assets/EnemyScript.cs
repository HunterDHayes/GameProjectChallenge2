using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnCollisionEnter2D(Collision2D col){
		if(col.gameObject.tag == "Player")
		{
			col.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-900.0f, 400.0f), ForceMode2D.Force);
			Physics2D.IgnoreCollision(col.collider, GetComponent<BoxCollider2D>());
		}
	}
}
