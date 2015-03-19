using UnityEngine;
using System.Collections;

public class ScrollingCredits : MonoBehaviour {

	public GameObject image;
	public int startPoint;
	public int endPoint;
	public float scrollSpeed;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		Vector3 position = image.transform.position;
		position.y += Time.deltaTime * this.scrollSpeed;

		if (position.y >= this.endPoint)
			position.y = this.startPoint;

		image.transform.position = position;
	}
}
