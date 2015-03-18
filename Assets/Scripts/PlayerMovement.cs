using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	public float jumpTime;
	public float jumpForce;
	public float jumpImpulse;
	
	private float currJumpTime = 0.0f;

	public float knockBackCount = 3;
	public float currKnockBack = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		Vector3 position = this.transform.position;
		position.x = Mathf.Lerp (this.transform.position.x, (-7.5f) * (this.currKnockBack / this.knockBackCount), Time.deltaTime * 2.0f);
		this.transform.position = position;

		if (Input.GetKeyDown (KeyCode.K))
			this.KnockBack ();

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
			
			if(dY > 0.0f && hit.collider.gameObject != gameObject)
			{
				currJumpTime = jumpTime;
				GetComponent<Rigidbody2D>().AddForce(new Vector2(0.0f, dY * jumpImpulse), ForceMode2D.Impulse);
			}
		}
	}

	public void KnockBack()
	{
		this.currKnockBack++;

		if (this.currKnockBack >= this.knockBackCount) {
			// TODO Do death stuff here
		}
	}
}
