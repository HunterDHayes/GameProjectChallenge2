using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour
{

	public float impulseUp;
	public float impulseBack;
	
	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (transform.position.y < -100.0f)
			Destroy (gameObject);
	}
	
	void DeactivateAllColliders() {
		for(int i = 0; i < transform.parent.childCount; i++) {
			Collider2D col = transform.parent.GetChild(i).GetComponent<Collider2D>();
			Rigidbody2D rb = transform.parent.GetChild(i).GetComponent<Rigidbody2D>();
			if(col) col.isTrigger = true;
			if(rb) rb.gravityScale = 1.0f;
		}
	}
	
	void IgnoreAllColliders(Collider2D other) {
		for(int i = 0; i < transform.parent.childCount; i++) {
			Collider2D col = transform.parent.GetChild(i).GetComponent<Collider2D>();
			if(col) Physics2D.IgnoreCollision(other, col);
		}
	}
	
	void OnCollisionEnter2D (Collision2D col)
	{
		if (col.gameObject.tag == "Player") 
		{
			if (col.gameObject.GetComponent<PlayerMovement> ().hasThePower == false) 
			{
				col.gameObject.GetComponent<Rigidbody2D> ().AddForce (new Vector2 (-impulseBack, impulseUp), ForceMode2D.Impulse);
				IgnoreAllColliders(col.collider);
				col.gameObject.GetComponent<PlayerMovement> ().KnockBack ();
			} 
			else 
			{
				DeactivateAllColliders();
			}
		}
	}
	
	void OnTriggerEnter2D (Collider2D col)
	{
		if (col.gameObject.tag == "Weapon") {
			DeactivateAllColliders();
		}
	}
}
