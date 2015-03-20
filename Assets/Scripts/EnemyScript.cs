using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour
{

	public float impulseUp;
	public float impulseBack;
	public Sprite AlternateSprite = null;
	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (transform.position.y < -20.0f || transform.position.x < -20.0f)
			Destroy (transform.parent.gameObject);
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
				col.gameObject.GetComponent<PlayerMovement>().StarPower(0.5f);
			} 
			else 
			{
				DeactivateAllColliders();
			}
		}
	}
	
	void OnTriggerEnter2D (Collider2D col)
	{
		if (col.gameObject.tag == "Weapon") 
		{
			DeactivateAllColliders();
			int currentKillcount = PlayerPrefs.GetInt("TotalKillCount");
			currentKillcount += 1;
			PlayerPrefs.SetInt("TotalKillCount",currentKillcount);

			if(AlternateSprite != null)
			{
				SpriteRenderer spriterenderer = GetComponent<SpriteRenderer>();
				spriterenderer.sprite = AlternateSprite;
			}
			GameObject soundManager = GameObject.FindGameObjectWithTag("SoundManager");
			
			if (soundManager)
			{
					if(transform.parent.name == "Enemy1(Clone)")
					{
					soundManager.SendMessage("PlaySfx", "Howl_1_B");
				}else if(transform.parent.name == "EnemyFlying(Clone)"){
					soundManager.SendMessage("PlaySfx", "Bird_BeingHit1");
				}else if(transform.parent.name == "Snake(Clone)"){
					soundManager.SendMessage("PlaySfx", "Snake_BeingHit2");
				}
				         }
		}
	}
}
