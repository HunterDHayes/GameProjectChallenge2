using UnityEngine;
using System.Collections;

public class Storyboard : MonoBehaviour
{

	public GameObject[] images;
	public int currentImage = 0;

	// Use this for initialization
	void Start ()
	{
		for (int i = 1; i < images.Length; i++) {
			images [i].gameObject.SetActive(false);
//			images [i].gameObject.GetComponent<SpriteRenderer> ().enabled = false;
		}
		images [currentImage].gameObject.SetActive(true);
//		images [this.currentImage].gameObject.GetComponent<SpriteRenderer> ().enabled = true;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKeyUp (KeyCode.Escape)) {
			Application.LoadLevel("MainMenu");
		}

		if (Input.anyKeyDown) {
			images [currentImage].gameObject.SetActive(false);
//			images [this.currentImage].gameObject.GetComponent<SpriteRenderer> ().enabled = false;
			this.currentImage++;

			if(this.currentImage == images.Length){
				Application.LoadLevel ("Gameplay");
				return;
			}

//			images [this.currentImage].gameObject.GetComponent<SpriteRenderer> ().enabled = true;
			images [currentImage].gameObject.SetActive(true);
		}
	}
}
