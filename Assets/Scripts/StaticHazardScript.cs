using UnityEngine;
using System.Collections;

public class StaticHazardScript : MonoBehaviour {

	public float impulseBack, impulseUp;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.y < -100.0f)
			Destroy (gameObject);
	}
	
	void OnCollisionEnter2D (Collision2D col)
	{
		if (col.gameObject.tag == "Player") 
		{
			if (col.gameObject.GetComponent<PlayerMovement> ().hasThePower == false) 
			{
				col.gameObject.GetComponent<Rigidbody2D> ().AddForce (new Vector2 (-impulseBack, impulseUp), ForceMode2D.Impulse);
				Physics2D.IgnoreCollision (col.collider, GetComponent<Collider2D> ());
				col.gameObject.GetComponent<PlayerMovement> ().KnockBack ();
			} 
			else 
			{
				GetComponent<Collider2D> ().isTrigger = true;
			}
		}
	}
}
