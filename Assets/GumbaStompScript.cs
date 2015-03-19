using UnityEngine;
using System.Collections;

public class GumbaStompScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void DeactivateAllColliders() {
		for(int i = 0; i < transform.parent.childCount; i++) {
			Collider2D col = transform.parent.GetChild(i).GetComponent<Collider2D>();
			if(col) col.isTrigger = true;
		}
	}
	
	void OnCollisionEnter2D(Collision2D col) {
		if(col.gameObject.tag == "Player") {
			DeactivateAllColliders();
		}
	}
}
