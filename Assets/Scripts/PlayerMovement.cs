using UnityEngine;
using System.Collections;


public class PlayerMovement : MonoBehaviour {

	public float jumpTime;
	public float jumpForce;
	public float jumpImpulse;
	public Color startColor;
	
	public GameObject sword;
	public GameObject swordCollider;
	public GameObject MainCamera;
	public float swingRotation;
	public float attackTime;
	private float lastAttack = 0.0f;
	private float currAttackTime = 0.0f;
	
	public float currJumpTime = 0.0f;

	public float knockBackCount = 3;
	public float currKnockBack = 0;

	public bool hasThePower = false;
	public float starPowerDuration = 3.0f;
	public float starPowerTimer = 0.0f;
	public ParticleSystem StarEffect = null;

	private Vector3 homePos;
	
	public float blinkTime = 0.0f;
	private float currBlink = 0.0f;
	private bool blinkVisible = false;

	// Use this for initialization
	void Start () 
	{
		homePos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
	
		currBlink -= Time.deltaTime;
		if(currBlink <= 0.0f) {
			blinkVisible = !blinkVisible;
			currBlink = blinkTime;
		}

		float percentKnockedBack;
		percentKnockedBack = currKnockBack / knockBackCount;
		
		float red = percentKnockedBack * 1.0f;
		
		SpriteRenderer[] sprite = GetComponentsInChildren<SpriteRenderer>();
		foreach (SpriteRenderer sp in sprite) 
		{
			Color color = new Color (1.0f, 1.0f, 1.0f, 1.0f);
			color.g =  startColor.g - red;
			color.b =  startColor.b - red;
			
			sp.color = color;
		}
		
		if(hasThePower) {
			foreach(SpriteRenderer ren in GetComponentsInChildren<SpriteRenderer>()) ren.enabled = blinkVisible;
		}
		else {
			foreach(SpriteRenderer ren in GetComponentsInChildren<SpriteRenderer>()) ren.enabled = true;
		}


		bool jump = false;
		bool attack = false;		
		for(int i = 0; i < Input.touchCount; i++) {
			if(Input.GetTouch(i).position.x > Screen.width / 2) {
				attack = true;
			}
			else {
				jump = true;
			}
		}

		Vector3 position = this.transform.position;
		position.x = Mathf.Lerp (this.transform.position.x, homePos.x + (-7.5f) * (this.currKnockBack / this.knockBackCount), Time.deltaTime * 4.0f);
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

		float dAttack = Input.GetAxis("Jump") + (attack ? 1.0f : 0.0f);
		
		if(currAttackTime > 0.0f){
			currAttackTime -= Time.deltaTime;
			
			float ratio = (currAttackTime / attackTime) * 2.0f;
			if(ratio > 1.0f) ratio = 2.0f - ratio;
			
			sword.transform.rotation = Quaternion.AngleAxis(ratio * swingRotation, new Vector3(0.0f, 0.0f, 1.0f));
		}
		else {
			foreach(Collider2D col in swordCollider.GetComponents<Collider2D>()) col.enabled = false;
		
			if(dAttack > 0.0f && lastAttack == 0.0f) {
				currAttackTime = attackTime;
				foreach(Collider2D col in swordCollider.GetComponents<Collider2D>()) col.enabled = true;
			}
		}
		lastAttack = dAttack;
		
	
		float dY = Input.GetAxis("Vertical") + (jump ? 1.0f : 0.0f);
		
		if(currJumpTime > 0.0f){
			currJumpTime -= Time.deltaTime;
			if(dY <= 0.0f) currJumpTime = 0.0f;
			GetComponent<Rigidbody2D>().AddForce(new Vector2(0.0f, dY * jumpForce), ForceMode2D.Force);
		}
		else{		
			RaycastHit2D hit = Physics2D.Linecast(
				new Vector2(transform.position.x, transform.position.y - 1.2f), 
				new Vector2(transform.position.x, transform.position.y)
			);
			
			if(dY > 0.0f && hit.collider.gameObject != gameObject && hit.collider.gameObject.tag == "Floor")
			{
				currJumpTime = jumpTime;
				GetComponent<Rigidbody2D>().AddForce(new Vector2(0.0f, dY * jumpImpulse), ForceMode2D.Impulse);
			}
		}

		Vector3 cameraPos = MainCamera.transform.position;
		cameraPos.y = transform.position.y + 3.5f;
		//MainCamera.transform.position = cameraPos;

		if (MainCamera.transform.position.y > 5.0f) {
			Vector3 pos = MainCamera.transform.position;
			pos.y = 5.0f;
			//MainCamera.transform.position = pos;
		} else if (MainCamera.transform.position.y < -5.0f) {
			Vector3 pos = MainCamera.transform.position;
			pos.y = -5.0f;
			//MainCamera.transform.position = pos;
		}


	}

	public void KnockBack()
	{
		this.currKnockBack++;


		if (this.currKnockBack >= this.knockBackCount) {
			// TODO Do death stuff here
			int deaths = PlayerPrefs.GetInt("TotalDeaths");
			PlayerPrefs.SetInt("TotalDeaths",deaths + 1);
			Application.LoadLevel("LoseMenu");

			float currentHighScore = PlayerPrefs.GetFloat("HighScore");
			float currentlength = 0.0f;
			GameObject floor = GameObject.Find("FLOORS");
			currentlength = floor.GetComponent<LevelScrollerScript>().metersCounter;
			if (currentlength > currentHighScore) 
			{
				PlayerPrefs.SetFloat("HighScore",currentlength);
			}



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
		if (StarEffect != null) 
		{
			//if(StarEffect.isStopped)
			//StarEffect.Play();
			//GetComponentInChildren<SpriteRenderer>().enabled = blinkVisible;
		}
		else {
			//GetComponentInChildren<SpriteRenderer>().enabled = true;
		}
		//this.gameObject.GetComponent<SpriteRenderer> ().color = Color.yellow;
	}
}
