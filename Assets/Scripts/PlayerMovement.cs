using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	public float jumpTime;
	public float jumpForce;
	public float jumpImpulse;
	
	public GameObject sword;
	public GameObject swordCollider;
	public GameObject MainCamera;
	public float swingRotation;
	public float attackTime;
	private float currAttackTime = 0.0f;
	
	public float currJumpTime = 0.0f;

	public float knockBackCount = 3;
	public float currKnockBack = 0;

	public bool hasThePower = false;
	public float starPowerDuration = 3.0f;
	public float starPowerTimer = 0.0f;
	
	private Vector3 homePos;

	// Use this for initialization
	void Start () 
	{
		homePos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {

		Vector3 position = this.transform.position;
		position.x = Mathf.Lerp (this.transform.position.x, homePos.x + (-7.5f) * (this.currKnockBack / this.knockBackCount), Time.deltaTime * 2.0f);
		this.transform.position = position;

		if (this.starPowerTimer > 0.0f) {
			this.starPowerTimer -= Time.deltaTime;

			if (this.starPowerTimer <= 0.0f) {
				this.hasThePower = false;
				//this.gameObject.GetComponent<SpriteRenderer> ().color = Color.white;
			}
		}
		if (Input.GetKeyDown (KeyCode.K))
			this.KnockBack ();

		float dAttack = Input.GetAxis("Jump");
		
		if(currAttackTime > 0.0f){
			currAttackTime -= Time.deltaTime;
			
			float ratio = (currAttackTime / attackTime) * 2.0f;
			if(ratio > 1.0f) ratio = 2.0f - ratio;
			
			sword.transform.rotation = Quaternion.AngleAxis(ratio * swingRotation, new Vector3(0.0f, 0.0f, 1.0f));
		}
		else {
			swordCollider.GetComponent<BoxCollider2D>().enabled = false;
		
			if(dAttack > 0.0f) {
				currAttackTime = attackTime;
				swordCollider.GetComponent<BoxCollider2D>().enabled = true;
			}
		}
		
	
		float dY = Input.GetAxis("Vertical");
		
		if(currJumpTime > 0.0f){
			currJumpTime -= Time.deltaTime;
			if(dY <= 0.0f) currJumpTime = 0.0f;
			GetComponent<Rigidbody2D>().AddForce(new Vector2(0.0f, dY * jumpForce), ForceMode2D.Force);
		}
		else{		
			RaycastHit2D hit = Physics2D.Linecast(
				new Vector2(transform.position.x, transform.position.y - 0.8f), 
				new Vector2(transform.position.x, transform.position.y)
			);
			
			if(dY > 0.0f && hit.collider.gameObject != gameObject && hit.collider.gameObject.layer != LayerMask.GetMask("Ground"))
			{
				currJumpTime = jumpTime;
				GetComponent<Rigidbody2D>().AddForce(new Vector2(0.0f, dY * jumpImpulse), ForceMode2D.Impulse);
			}
		}

		Vector3 cameraPos = MainCamera.transform.position;
		cameraPos.y = transform.position.y + 3.5f;
		MainCamera.transform.position = cameraPos;

		if (MainCamera.transform.position.y > 5.0f) {
			Vector3 pos = MainCamera.transform.position;
			pos.y = 5.0f;
			MainCamera.transform.position = pos;
		} else if (MainCamera.transform.position.y < -5.0f) {
			Vector3 pos = MainCamera.transform.position;
			pos.y = -5.0f;
			MainCamera.transform.position = pos;
		}


	}

	public void KnockBack()
	{
		this.currKnockBack++;

		if (this.currKnockBack >= this.knockBackCount) {
			// TODO Do death stuff here
			Application.LoadLevel("LoseMenu");
		}
	}

	public void HealthUp()
	{
		this.currKnockBack = Mathf.Max (0.0f, this.currKnockBack - 1.0f);
	}

	public void StarPower()
	{
		this.starPowerTimer = this.starPowerDuration;
		this.hasThePower = true;
		//this.gameObject.GetComponent<SpriteRenderer> ().color = Color.yellow;
	}
}
