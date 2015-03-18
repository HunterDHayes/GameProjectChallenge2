using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour {

	public float impulseUp;
	public float impulseBack;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(transform.position.y < -100.0f)
			Destroy(gameObject);
	}
	
	void OnCollisionEnter2D(Collision2D col){
		if(col.gameObject.tag == "Player")
		{
			col.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-impulseBack, impulseUp), ForceMode2D.Impulse);
			Physics2D.IgnoreCollision(col.collider, GetComponent<BoxCollider2D>());
			col.gameObject.GetComponent<PlayerMovement>().KnockBack();
		}
	}
	
	void OnTriggerEnter2D(Collider2D col){
		if(col.gameObject.tag == "Weapon") {
			GetComponent<BoxCollider2D>().isTrigger = true;
		}
	}
}
